using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Common;

namespace TargetSettingTool.Domain.Entities
{
    public class UserParameter : CommonAuditProperties
    {
        public Guid UserId { get; set; }

        public Guid ParameterId { get; set; }

        public Guid? BranchId { get; set; }

        public double TargetAmount { get; set; }

        public bool IsVerified { get; set; }

        public Guid? VerifiedById { get; set; }

        public DateTime? VerifiedTime { get; set; }

        public bool IsValidated { get; set; }
        
        public Guid? ValidatedById { get; set; }

        public DateTime? ValidatedTime { get; set; }

        [MaxLength(100, ErrorMessage = "Reason cannot be larger than 100 characters")]
        public string? Reason { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ParameterId")]
        public virtual Parameter Parameter { get; set; }

        [ForeignKey("BranchId")]
        public virtual Branch? Branch { get; set; }

        [ForeignKey("VerifiedById")]
        public virtual User? VerifiedBy { get; set; }

        [ForeignKey("ValidatedById")]
        public virtual User? ValidatedBy { get; set; }
    }
}
