using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Courses.Domain.Repositories;
using Asimov.API.Shared.Domain.Repositories;
using Asimov.API.Units.Domain.Models;
using Asimov.API.Units.Domain.Repositories;
using Asimov.API.Units.Domain.Services;
using Asimov.API.Units.Domain.Services.Communication;

namespace Asimov.API.Units.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;

        public UnitService(IUnitRepository unitRepository, IUnitOfWork unitOfWork, ICourseRepository courseRepository)
        {
            _unitRepository = unitRepository;
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Unit>> ListAsync()
        {
            return await _unitRepository.ListAsync();   
        }
        
        public async Task<IEnumerable<Unit>> ListByCourseAsync(int courseId)
        {
            return await _unitRepository.FindByCourseId(courseId);
        }

        public async Task<UnitResponse> SaveAsync(Unit unit)
        {
            var existingCourse = await _courseRepository.FindByIdAsync(unit.CourseId);
            if (existingCourse == null)
            {
                return new UnitResponse("Course not found.");
            }
            try
            {
                await _unitRepository.AddAsync(unit);
                await _unitOfWork.CompleteAsync();

                return new UnitResponse(unit);
            }
            catch (Exception e)
            {
                return new UnitResponse($"Error while saving unit: {e.Message}");
            }
        }

        public async Task<UnitResponse> UpdateAsync(int id, Unit unit)
        {
            var existingUnit = await _unitRepository.FindByIdAsync(id);

            if (existingUnit == null)
                return new UnitResponse("Unit not found.");
            existingUnit.Title = unit.Title;
            existingUnit.Description = unit.Description;
            existingUnit.CourseId = unit.CourseId;

            try
            {
                _unitRepository.Update(existingUnit);
                await _unitOfWork.CompleteAsync();

                return new UnitResponse(existingUnit);
            }
            catch (Exception e)
            {
                return new UnitResponse($"Error while updating unit: {e.Message}");
            }
        }

        public async Task<UnitResponse> DeleteAsync(int id)
        {
            var existingUnit = await _unitRepository.FindByIdAsync(id);

            if (existingUnit == null)
                return new UnitResponse("Unit not found.");

            try
            {
                _unitRepository.Remove(existingUnit);
                await _unitOfWork.CompleteAsync();

                return new UnitResponse(existingUnit);
            }
            catch (Exception e)
            {
                return new UnitResponse($"Error while deleting unit: {e.Message}");

            }
        }

        public async Task<UnitResponse> GetByIdAsync(int id)
        {
            var existingUnit = await _unitRepository.FindByIdAsync(id);

            if (existingUnit == null)
                return new UnitResponse("Unit not found.");

            return new UnitResponse(existingUnit);
        }
    }
}