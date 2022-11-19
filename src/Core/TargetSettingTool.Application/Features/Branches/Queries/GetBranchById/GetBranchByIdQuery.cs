using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Branches.Queries.GetBranchById
{
    public class GetBranchByIdQuery : IRequest<Response<BranchDto>>
    {
        public Guid Id { get; set; }
    }
}
