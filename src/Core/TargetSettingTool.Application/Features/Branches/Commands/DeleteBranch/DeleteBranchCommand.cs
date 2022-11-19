using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Branches.Commands.DeleteBranch
{
    public class DeleteBranchCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}
