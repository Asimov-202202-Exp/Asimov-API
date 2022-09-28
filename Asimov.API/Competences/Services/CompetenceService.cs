using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Competences.Domain.Models;
using Asimov.API.Competences.Domain.Repositories;
using Asimov.API.Competences.Domain.Services;
using Asimov.API.Competences.Domain.Services.Communication;
using Asimov.API.Courses.Domain.Repositories;
using Asimov.API.Security.Exceptions;
using Asimov.API.Shared.Domain.Repositories;

namespace Asimov.API.Competences.Services
{
    public class CompetenceService : ICompetenceService
    {
        private readonly ICompetenceRepository _competenceRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CompetenceService(ICompetenceRepository competenceRepository, IUnitOfWork unitOfWork, ICourseRepository courseRepository)
        {
            _competenceRepository = competenceRepository;
            _unitOfWork = unitOfWork;
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Competence>> ListAsync()
        {
            return await _competenceRepository.ListAsync();
        }

        public async Task<IEnumerable<Competence>> ListByCourseIdAsync(int courseId)
        {
            return await _competenceRepository.FindByCourseId(courseId);
        }

        public async Task<CompetenceResponse> SaveAsync(Competence competence)
        {
            var existingCourse = _courseRepository.FindByIdAsync(competence.CourseId);

            if (existingCourse.Result == null)
                return new CompetenceResponse("Invalid Course");
            
            if (_competenceRepository.ExistByTitle(competence.Title))
                throw new AppException
                    ("This competence belongs to another Course");
            
            try
            {
                await _competenceRepository.AddAsync(competence);
                await _unitOfWork.CompleteAsync();

                return new CompetenceResponse(competence);
            }
            catch (Exception e)
            {
                return new CompetenceResponse($"An error occurred while saving competence: {e.Message}");
            }
        }

        public async Task<CompetenceResponse> UpdateAsync(int id, Competence competence)
        {
            var existingCompetence = await _competenceRepository.FindByIdAsync(id);

            if (existingCompetence == null)
                return new CompetenceResponse("Competence not found");
            
            var existingCourse = _courseRepository.FindByIdAsync(competence.CourseId);

            if (existingCourse.Result == null)
                return new CompetenceResponse("Invalid Course");
            
            existingCompetence.Title = competence.Title;
            existingCompetence.Description = competence.Description;

            try
            {
                _competenceRepository.Update(existingCompetence);
                await _unitOfWork.CompleteAsync();

                return new CompetenceResponse(existingCompetence);
            }
            catch (Exception e)
            {
                return new CompetenceResponse($"An error occurred while updating competence: {e.Message}");
            }
        }

        public async Task<CompetenceResponse> DeleteAsync(int id)
        {
            var existingCompetence = await _competenceRepository.FindByIdAsync(id);

            if (existingCompetence == null)
                return new CompetenceResponse("Competence not found");

            try
            {
                _competenceRepository.Remove(existingCompetence);
                await _unitOfWork.CompleteAsync();

                return new CompetenceResponse(existingCompetence);
            }
            catch (Exception e)
            {
                return new CompetenceResponse($"An error occurred while deleting competence: {e.Message}");
            }
        }
    }
}