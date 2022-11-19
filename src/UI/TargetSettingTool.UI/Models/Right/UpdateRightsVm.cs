using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace TargetSettingTool.UI.Models.Right
{
    public class UpdateRightsVm
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Za-z ]+$", ErrorMessage = "Name should contain alphabet letters only.")]
        [Remote("IsRightExist", "Right",AdditionalFields = "Id", HttpMethod = "GET", ErrorMessage= "Right already exists.")]
        [DisplayName("Right")]
        public string Name { get; set; }
        public string LoggedInUser { get; set; }

    }
}
