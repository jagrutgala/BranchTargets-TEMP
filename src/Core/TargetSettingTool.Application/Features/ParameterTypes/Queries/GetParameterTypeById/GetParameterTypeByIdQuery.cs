using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.ParameterTypes.Queries.GetParameterTypeById
{
    public class GetParameterTypeByIdQuery : IRequest<Response<GetParameterTypeByIdVm>>
    {
        public Guid Id { get; set; }
    }
}
