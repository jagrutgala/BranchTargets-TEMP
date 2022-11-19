using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Rights.Queries.GetRightByName
{
    public class GetRightByNameQuery:IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Right { get; set; }
    }
}
