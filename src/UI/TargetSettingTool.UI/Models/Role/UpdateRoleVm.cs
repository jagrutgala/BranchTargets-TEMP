using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace TargetSettingTool.UI.Models.Role
{
    public class UpdateRoleVm
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Name should contain alphabet letters only.")]
        [Remote("IsRoleExists", "Role", AdditionalFields = "Id", HttpMethod ="GET", ErrorMessage = "Role already exists.")]
        [DisplayName("Role")]
        public string Name { get; set; }
        public Guid LoggedInUser { get; set; }
    }
}
