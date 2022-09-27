using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asimov.API.Shared.Persistence.Contexts;
using Asimov.API.Shared.Persistence.Repositories;
using Asimov.API.Units.Domain.Models;
using Asimov.API.Units.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Asimov.API.Units.Persistence.Repositories
{
    public class UnitRepository : BaseRepository, IUnitRepository
    {
        public UnitRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Unit>> ListAsync()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task AddAsync(Unit unit)
        {
            await _context.Units.AddAsync(unit);
        }

        public async Task<Unit> FindByIdAsync(int id)
        {
            return await _context.Units.FindAsync(id);
        }

        public void Update(Unit unit)
        {
            _context.Units.Update(unit);
        }

        public void Remove(Unit unit)
        {
            _context.Units.Remove(unit);
        }
        
        public async Task<IEnumerable<Unit>> FindByCourseId(int id)
        {
            return await _context.Units
                .Where(p => p.CourseId == id)
                .Include(p => p.Course)
                .ToListAsync();
        }
    }
}