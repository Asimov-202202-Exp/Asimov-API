using System.Collections.Generic;
using System.Threading.Tasks;
using Asimov.API.Activities.Domain.Models;
using Asimov.API.Activities.Domain.Services.Communication;

namespace Asimov.API.Activities.Domain.Services
{
    public interface IActivityService
    {
        Task<IEnumerable<Activity>> ListAsync();
        Task<IEnumerable<Activity>> ListByCourseIdAsync(int courseId);
        Task<ActivityResponse> SaveAsync(Activity activity);
        Task<ActivityResponse> UpdateAsync(int id, Activity activity);
        Task<ActivityResponse> DeleteAsync(int id);
    }
}