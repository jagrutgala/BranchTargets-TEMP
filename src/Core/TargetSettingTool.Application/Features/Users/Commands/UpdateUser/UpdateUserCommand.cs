using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommand: IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid RoleId { get; set; }
        public Guid? RightId { get; set; }
        public List<Guid>? Branches { get; set; }
        public Guid LoggedInUserId { get; set; }
    }
}
