using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TargetSettingTool.UI.Models.Branch
{
    public class BranchDeleteRequestModel
    {
        [Required]
        public Guid Id { get; set; }
        public Guid LoggedInUserId { get; set; } = Guid.Empty;
    }
}
