using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Branches.Commands.CreateBranch
{
    public class CreateBranchCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; }
        public Guid RegionId { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}
