using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.ParameterTypes.Queries.GetAllParamterTypes
{
    public class GetAllParameterTypesQuery : IRequest<Response<IEnumerable<ParameterTypesDto>>>
    {

    }
}
