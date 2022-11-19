using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.UserBranches.Queries.GetBranchDropdown
{
    public class GetBranchDropdownQuery : IRequest<Response<IEnumerable<BranchDto>>>
    {
    }
}
