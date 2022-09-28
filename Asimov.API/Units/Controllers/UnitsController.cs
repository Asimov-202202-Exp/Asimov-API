using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Shared.Extensions;
using Asimov.API.Units.Domain.Models;
using Asimov.API.Units.Domain.Services;
using Asimov.API.Units.Resources;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asimov.API.Units.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public UnitsController(IUnitService unitService, IMapper mapper)
        {
            _unitService = unitService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<UnitResource>> GetAllAsync()
        {
            var units = await _unitService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Unit>, IEnumerable<UnitResource>>(units);

            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveUnitResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var unit = _mapper.Map<SaveUnitResource, Unit>(resource);

            var result = await _unitService.SaveAsync(unit);

            if (!result.Success)
                return BadRequest(result.Message);

            var unitResource = _mapper.Map<Unit, UnitResource>(result.Resource);
            return Ok(unitResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveUnitResource resource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorMessages());
            }

            var unit = _mapper.Map<SaveUnitResource, Unit>(resource);

            var result = await _unitService.UpdateAsync(id, unit);

            if (!result.Success)
                return BadRequest(result.Message);

            var unitResource = _mapper.Map<Unit, UnitResource>(result.Resource);
            return Ok(unitResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _unitService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var unitResource = _mapper.Map<Unit, UnitResource>(result.Resource);
            return Ok(unitResource);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _unitService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var unitResource = _mapper.Map<Unit, UnitResource>(result.Resource);
            return Ok(unitResource);
        }
    }
}