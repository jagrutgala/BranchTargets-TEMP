using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Rights.Queries.GetRightById
{
    public class GetRightByIdQuery:IRequest<Response<GetRightByIdVm>>
    {
        public Guid Id { get; set; }

    }
}
