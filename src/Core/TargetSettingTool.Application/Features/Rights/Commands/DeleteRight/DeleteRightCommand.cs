using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Rights.Commands.DeleteRight
{
    public class DeleteRightCommand:IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public Guid LoggedIn { get; set; }
    }
}
