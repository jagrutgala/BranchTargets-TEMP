using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Common;

namespace TargetSettingTool.Domain.Entities
{
    public class BranchTarget : CommonAuditProperties
    { 
        public Guid UserParameterId { get; set; }

        public int AchievedTargetAmount { get; set; }

        [ForeignKey("UserParameterId")]
        public virtual UserParameter UserParameter { get; set; }

    }
}
