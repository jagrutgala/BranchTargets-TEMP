using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Branches.Queries.GetBranchByName
{
    public class GetBranchByNameQuery : IRequest<Response<BranchDto>>
    {
        public string Name { get; set; }
    }
}
