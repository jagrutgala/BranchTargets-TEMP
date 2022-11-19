using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.BranchTargets.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.BranchTargets.Queries
{
    public class GetAllBranchTargetsQuery : IRequest<Response<IEnumerable<BranchTargetDto>>>
    {
        public Guid BranchId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
