using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Parameters.Queries.GetParameterByType
{
    public class GetParameterByTypeQuery : IRequest<Response<bool>>
    {
        public string Name { get; set; }
    }
}
