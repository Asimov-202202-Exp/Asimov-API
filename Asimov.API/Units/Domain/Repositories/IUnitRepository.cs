using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Units.Domain.Models;

namespace Asimov.API.Units.Domain.Repositories
{
    public interface IUnitRepository
    {
        Task<IEnumerable<Unit>> ListAsync();
        Task AddAsync(Unit unit);
        Task<Unit> FindByIdAsync(int id);
        void Update(Unit unit);
        void Remove(Unit unit);
    }
}