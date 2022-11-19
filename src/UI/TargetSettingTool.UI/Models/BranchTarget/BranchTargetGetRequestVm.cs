using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TargetSettingTool.UI.Models.BranchTarget
{
    public class BranchTargetGetRequestVm
    {
        public Guid BranchId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
