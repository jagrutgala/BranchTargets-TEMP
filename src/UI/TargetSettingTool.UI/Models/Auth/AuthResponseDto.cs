
using TargetSettingTool.UI.Models.Right;
using TargetSettingTool.UI.Models.Role;

namespace TargetSettingTool.UI.Models.Auth
{
    public class AuthResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid RoleId { get; set; }

        public Guid? RightId { get; set; }

        public GetRightVM Right { get; set; }
        public GetAllRolesVm Role { get; set; }

    }
}
