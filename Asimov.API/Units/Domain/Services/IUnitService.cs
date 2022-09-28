using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Units.Domain.Models;
using Asimov.API.Units.Domain.Services.Communication;

namespace Asimov.API.Units.Domain.Services
{
    public interface IUnitService
    {
        Task<IEnumerable<Unit>> ListAsync();
        Task<IEnumerable<Unit>> ListByCourseAsync(int courseId);
        Task<UnitResponse> SaveAsync(Unit unit);
        Task<UnitResponse> UpdateAsync(int id, Unit unit);
        Task<UnitResponse> DeleteAsync(int id);
        Task<UnitResponse> GetByIdAsync(int id);
    }
}