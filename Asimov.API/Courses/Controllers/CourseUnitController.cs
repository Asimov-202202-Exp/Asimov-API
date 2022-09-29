using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Units.Domain.Models;
using Asimov.API.Units.Domain.Services;
using Asimov.API.Units.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asimov.API.Courses.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/courses/{courseId}/units")]
    public class CourseUnitController: ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public CourseUnitController(IUnitService unitService, IMapper mapper)
        {
            _unitService = unitService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UnitResource>> GetAllByCourseIdAsync(int courseId)
        {
            var unit = await _unitService.ListByCourseAsync(courseId);
            var resources = _mapper.Map<IEnumerable<Unit>, IEnumerable<UnitResource>>(unit);
            return resources;
        }
    }
}