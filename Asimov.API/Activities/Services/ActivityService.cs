using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Activities.Domain.Models;
using Asimov.API.Activities.Domain.Repositories;
using Asimov.API.Activities.Domain.Services;
using Asimov.API.Activities.Domain.Services.Communication;
using Asimov.API.Courses.Domain.Repositories;
using Asimov.API.Security.Exceptions;
using Asimov.API.Shared.Domain.Repositories;

namespace Asimov.API.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivityService(IActivityRepository activityRepository, ICourseRepository courseRepository, IUnitOfWork unitOfWork)
        {
            _activityRepository = activityRepository;
            _courseRepository = courseRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Activity>> ListAsync()
        {
            return await _activityRepository.ListAsync();
        }

        public async Task<IEnumerable<Activity>> ListByCourseIdAsync(int courseId)
        {
            return await _activityRepository.FindByCourseId(courseId);
        }

        public async Task<ActivityResponse> SaveAsync(Activity activity)
        {
            
            var existingCourse = _courseRepository.FindByIdAsync(activity.CourseId);

            if (existingCourse.Result == null)
                return new ActivityResponse("Invalid Course");
            
            if (_activityRepository.ExistByValue(activity.Value))
                throw new AppException
                    ("This activity is already assigned to another course");

            try
            {
                await _activityRepository.AddAsync(activity);
                await _unitOfWork.CompleteAsync();

                return new ActivityResponse(activity);
            }
            catch (Exception e)
            {
                return new ActivityResponse($"An error occurred while saving the item: {e.Message}");
            }
        }

        public async Task<ActivityResponse> UpdateAsync(int id, Activity activity)
        {
            var existingItem = await _activityRepository.FindByIdAsync(id);

            if (existingItem == null)
                return new ActivityResponse("Item not found");
            
            var existingCourse = _courseRepository.FindByIdAsync(activity.CourseId);

            if (existingCourse == null) 
                return new ActivityResponse("Invalid Course");

            existingItem.Name = activity.Name;
            existingItem.Value = activity.Value;
            existingItem.State = activity.State;
            existingItem.CourseId = activity.CourseId;

            try
            {
                _activityRepository.Update(existingItem);
                await _unitOfWork.CompleteAsync();

                return new ActivityResponse(existingItem);
            }
            catch (Exception e)
            {
                return new ActivityResponse($"An error occurred while updating the item: {e.Message}");
            }
        }

        public async Task<ActivityResponse> DeleteAsync(int id)
        {
            var existingItem = await _activityRepository.FindByIdAsync(id);

            if (existingItem == null)
                return new ActivityResponse("Item not found");
            
            try
            {
                _activityRepository.Remove(existingItem);
                await _unitOfWork.CompleteAsync();

                return new ActivityResponse(existingItem);
            }
            catch (Exception e)
            {
                return new ActivityResponse($"An error occurred while deleting the item: {e.Message}");
            }
        }
    }
}