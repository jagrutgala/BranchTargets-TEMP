using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Branches.Queries.GetAllBranch
{
    public class GetAllBranchQuery : IRequest<Response<IEnumerable<BranchDto>>>
    {
    }
}
