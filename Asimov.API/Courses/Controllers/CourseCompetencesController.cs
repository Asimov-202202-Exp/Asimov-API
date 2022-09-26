using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Competences.Domain.Models;
using Asimov.API.Competences.Domain.Services;
using Asimov.API.Competences.Resources;
using Asimov.API.Courses.Domain.Services;
using Asimov.API.Security.Authorization.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asimov.API.Courses.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/courses/{courseId}/competences")]
    public class CourseCompetencesController : ControllerBase
    {
        private readonly ICompetenceService _competenceService;
        private readonly IMapper _mapper;

        public CourseCompetencesController(IMapper mapper, ICompetenceService competenceService)
        {
            _mapper = mapper;
            _competenceService = competenceService;
        }
        
        [HttpGet]
        public async Task<IEnumerable<CompetenceResource>> GetAllByCourseIdAsync(int courseId)
        {
            var competences = await _competenceService.ListByCourseIdAsync(courseId);
            var resources = _mapper.Map<IEnumerable<Competence>, IEnumerable<CompetenceResource>>(competences);

            return resources;
        }
    }
}