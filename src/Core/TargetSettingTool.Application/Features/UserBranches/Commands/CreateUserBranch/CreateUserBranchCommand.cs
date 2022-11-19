using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.UserBranches.Commands.CreateUserBranch
{
    public class CreateUserBranchCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid BranchId { get; set; }

        public Guid LoggedInUserId { get; set; }
    }
}
