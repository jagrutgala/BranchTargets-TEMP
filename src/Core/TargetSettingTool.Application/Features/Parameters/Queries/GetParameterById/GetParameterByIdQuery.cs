using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.Parameters.Queries.GetParameterById;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Parameters.Queries.GetParametersById
{
    public class GetParameterByIdQuery : IRequest<Response<GetParameterByIdVm>>
    {
        public Guid Id { get; set; }
        
    }
}
