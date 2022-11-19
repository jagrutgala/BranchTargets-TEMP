using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.UserBranches.Queries.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.UserBranches.Queries.GetAllBranchesByUserId
{
    public class GetAllBranchesByUserIdQuery : IRequest<Response<IEnumerable<UserBranchDto>>>
    {
        public Guid UserId { get; set; }
    }
}
