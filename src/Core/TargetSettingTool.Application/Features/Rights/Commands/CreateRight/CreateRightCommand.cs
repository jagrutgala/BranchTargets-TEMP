using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Rights.Commands.CreateRight
{
    public class CreateRightCommand:IRequest<Response<Guid>>
    {
        public string Name { get; set; }
        public Guid LoggedIn { get; set; }
    }
}
