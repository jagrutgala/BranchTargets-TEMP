using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Parameters.Queries.GetAllParameters
{
    public class GetAllParametersQuery : IRequest<Response<IEnumerable<ParameterDto>>>
    {
    }
}
