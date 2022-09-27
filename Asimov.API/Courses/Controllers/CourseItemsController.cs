using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Activities.Domain.Models;
using Asimov.API.Activities.Domain.Services;
using Asimov.API.Activities.Resources;
using Asimov.API.Security.Authorization.Attributes;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asimov.API.Courses.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/courses/{courseId}/items")]
    public class CourseItemsController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IMapper _mapper;

        public CourseItemsController(IActivityService activityService, IMapper mapper)
        {
            _activityService = activityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ActivityResource>> GetAllByCourseIdAsync(int courseId)
        {
            var items = await _activityService.ListByCourseIdAsync(courseId);
            var resources = _mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityResource>>(items);

            return resources;
        }
    }
}