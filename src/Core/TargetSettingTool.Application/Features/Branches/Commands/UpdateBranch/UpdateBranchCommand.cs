using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Branches.Commands.UpdateBranch
{
    public class UpdateBranchCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid RegionId { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}
