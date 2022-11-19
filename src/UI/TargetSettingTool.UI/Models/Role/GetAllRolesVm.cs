using System.ComponentModel;

namespace TargetSettingTool.UI.Models.Role
{
    public class GetAllRolesVm
    {
        public Guid Id { get; set; }
        [DisplayName("Role")]
        public string Name { get; set; }

    }
}
