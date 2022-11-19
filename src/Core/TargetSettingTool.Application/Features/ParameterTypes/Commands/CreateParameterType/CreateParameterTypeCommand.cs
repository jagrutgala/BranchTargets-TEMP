using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.ParameterTypes.Commands.CreateParameterType
{
    public class CreateParameterTypeCommand : IRequest<Response<Guid>>
    {
        public string Name { get; set; }
        public Guid LoggedInUser { get; set; }
    }
}
