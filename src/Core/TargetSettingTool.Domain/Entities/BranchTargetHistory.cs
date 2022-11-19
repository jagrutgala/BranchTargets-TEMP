using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetSettingTool.Domain.Entities
{
    public class BranchTargetHistory
    {
        [Key]
        public Guid HistoryId { get; set; }

        public Guid UserParameterId { get; set; }

        public int AchievedTargetAmount { get; set; }
        public Guid Id { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }

        [ForeignKey("Id")]
        public virtual BranchTarget BranchTarget { get; set; }
    }
}
