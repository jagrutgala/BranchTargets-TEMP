using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Features.Parameters.Commands.CreateParameter;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Parameters.Commands.CreateParameters
{
    public class CreateParametersCommand : IRequest<Response<IEnumerable<Guid>>>
    {
        public List<CreateParameterCommand> Parameters { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}
