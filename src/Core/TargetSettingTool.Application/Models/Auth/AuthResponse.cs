using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TargetSettingTool.Application.Features.Rights.Queries.GetAllRights;
using TargetSettingTool.Application.Features.Roles.Queries.GetAllRoles;

namespace TargetSettingTool.Application.Models.Auth
{
    public class AuthResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }

        public Guid? RightId { get; set; }

        public GetAllRightsVm? Right { get; set; }
        public GetAllRolesVm? Role {get; set;}
    }
}
