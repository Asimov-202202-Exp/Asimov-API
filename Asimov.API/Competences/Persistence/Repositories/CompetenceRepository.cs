using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asimov.API.Competences.Domain.Models;
using Asimov.API.Competences.Domain.Repositories;
using Asimov.API.Shared.Persistence.Contexts;
using Asimov.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Asimov.API.Competences.Persistence.Repositories
{
    public class CompetenceRepository : BaseRepository, ICompetenceRepository
    {
        public CompetenceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Competence>> ListAsync()
        {
            return await _context.Competences.ToListAsync();
        }

        public async Task AddAsync(Competence competence)
        {
            await _context.Competences.AddAsync(competence);
        }

        public async Task<Competence> FindByIdAsync(int id)
        {
            return await _context.Competences.FindAsync(id);
        }

        public async Task<IEnumerable<Competence>> FindByCourseId(int courseId)
        {
            return await _context.Competences
                .Where(p => p.CourseId == courseId)
                .Include(p => p.Course)
                .ToListAsync();
        }

        public async Task<Competence> FindByTitleAsync(string title)
        {
            return await _context.Competences.SingleOrDefaultAsync(p => p.Title == title);
        }

        public bool ExistByTitle(string title)
        {
            return _context.Competences.Any(p => p.Title == title);
        }

        public void Update(Competence competence)
        {
            _context.Competences.Update(competence);
        }

        public void Remove(Competence competence)
        {
            _context.Competences.Remove(competence);
        }
    }
}