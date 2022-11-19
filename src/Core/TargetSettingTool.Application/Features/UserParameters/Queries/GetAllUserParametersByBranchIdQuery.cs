using MediatR;

using TargetSettingTool.Application.Features.UserParameters.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.UserParameters.Queries
{
    public class GetAllUserParametersByBranchIdQuery : IRequest<Response<IEnumerable<UserParameterDto>>>
    {
        public Guid BranchId { get; set; }
    }
}
