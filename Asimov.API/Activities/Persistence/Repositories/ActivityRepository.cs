using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asimov.API.Activities.Domain.Models;
using Asimov.API.Activities.Domain.Repositories;
using Asimov.API.Shared.Persistence.Contexts;
using Asimov.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Asimov.API.Activities.Persistence.Repositories
{
    public class ActivityRepository : BaseRepository, IActivityRepository
    {
        public ActivityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Activity>> ListAsync()
        {
            return await _context.Activities
                .Include(p => p.Course)
                .ToListAsync();
        }

        public async Task AddAsync(Activity activity)
        {
            await _context.Activities.AddAsync(activity);
        }

        public async Task<Activity> FindByIdAsync(int id)
        {
            return await _context.Activities
                .Include(p => p.Course)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Activity>> FindByCourseId(int courseId)
        {
            return await _context.Activities
                .Where(p => p.CourseId == courseId)
                .Include(p => p.Course)
                .ToListAsync();
        }

        public void Update(Activity activity)
        {
            _context.Activities.Update(activity);
        }

        public void Remove(Activity activity)
        {
            _context.Activities.Remove(activity);
        }
    }
}