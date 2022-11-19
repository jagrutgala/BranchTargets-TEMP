using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetSettingTool.Domain.Entities
{
    public class UserParameterHistory
    {
        [Key]
        public Guid HistoryId { get; set; }

        public Guid Id { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public Guid? LastModifiedBy { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public bool IsDeleted { get; set; } = false;

        public Guid? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public DateTime HistoryDate { get; set; }

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

        [ForeignKey("Id")]
        public virtual UserParameter UserParameter { get; set; }
    }
}
