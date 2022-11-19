using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.UserBranches.Commands.UpdateUserBranch
{
    public class UpdateUserBranchCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid BranchId { get; set; }

        public Guid LoggedInUserId { get; set; }
    }
}
