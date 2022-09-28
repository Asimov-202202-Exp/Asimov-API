using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Activities.Domain.Models;

namespace Asimov.API.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> ListAsync();
        Task AddAsync(Activity activity);
        Task<Activity> FindByIdAsync(int id);
        Task<IEnumerable<Activity>> FindByCourseId(int courseId);
        void Update(Activity activity);
        void Remove(Activity activity);
        public bool ExistByValue(string value);
    }
}