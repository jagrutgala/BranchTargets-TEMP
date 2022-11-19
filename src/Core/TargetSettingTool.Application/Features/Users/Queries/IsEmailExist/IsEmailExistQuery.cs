using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Users.Queries.IsEmailExist
{
    public class IsEmailExistQuery:IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
    }
}
