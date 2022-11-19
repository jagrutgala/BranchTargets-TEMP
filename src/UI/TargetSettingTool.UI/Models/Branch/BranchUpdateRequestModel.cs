using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

namespace TargetSettingTool.UI.Models.Branch
{
    public class BranchUpdateRequestModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Remote("IsBranchNameAvailableEdit", "Branch", ErrorMessage = "branch name already exists", AdditionalFields = "Id")]
        [DisplayName("Branch")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Region")]
        public Guid? RegionId { get; set; }

        public Guid LoggedInUserId { get; set; } = Guid.Empty;
    }
}
