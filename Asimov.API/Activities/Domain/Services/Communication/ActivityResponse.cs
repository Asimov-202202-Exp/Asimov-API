using Asimov.API.Activities.Domain.Models;
using Asimov.API.Shared.Domain.Services.Communication;

namespace Asimov.API.Activities.Domain.Services.Communication
{
    public class ActivityResponse : BaseResponse<Activity>
    {
        public ActivityResponse(string message) : base(message)
        {
        }

        public ActivityResponse(Activity resource) : base(resource)
        {
        }
    }
}