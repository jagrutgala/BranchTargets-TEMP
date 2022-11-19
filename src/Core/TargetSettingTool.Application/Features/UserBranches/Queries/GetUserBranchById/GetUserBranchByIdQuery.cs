using MediatR;

using TargetSettingTool.Application.Features.UserBranches.Queries.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.UserBranches.Queries.GetUserBranchById
{
    public class GetUserBranchByIdQuery : IRequest<Response<UserBranchDto>>
    {
        public Guid Id { get; set; }
    }
}
