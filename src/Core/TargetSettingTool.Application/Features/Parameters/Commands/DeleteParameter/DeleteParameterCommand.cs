using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Parameters.Commands.DeleteParameter
{
    public class DeleteParameterCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}
