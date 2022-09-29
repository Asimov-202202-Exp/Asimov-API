using Asimov.API.Shared.Domain.Services.Communication;
using Asimov.API.Units.Domain.Models;

namespace Asimov.API.Units.Domain.Services.Communication
{
    public class UnitResponse : BaseResponse<Unit>
    {
        public UnitResponse(Unit unit) : base(unit) { }
        public UnitResponse(string message) : base(message) { }
    }
}