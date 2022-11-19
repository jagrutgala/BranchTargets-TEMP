using MediatR;

using TargetSettingTool.Application.Features.UserBranches.Queries.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.UserBranches.Queries.GetAllUserBranch
{
    public class GetAllUserBranchQuery : IRequest<Response<IEnumerable<UserBranchDto>>>
    {
    }
}
