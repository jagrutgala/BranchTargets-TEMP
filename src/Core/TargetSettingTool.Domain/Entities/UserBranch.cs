using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Common;

namespace TargetSettingTool.Domain.Entities
{
    public class UserBranch : CommonAuditProperties
    {
        public Guid UserId { get; set; }

        public Guid BranchId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }
    }
}
