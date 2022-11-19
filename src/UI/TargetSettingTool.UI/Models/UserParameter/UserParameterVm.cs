using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.UI.Models.Branch;
using TargetSettingTool.UI.Models.Parameter;

namespace TargetSettingTool.UI.Models.UserParameter
{
    public class UserParameterVm
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

        public string? Reason { get; set; }

        public virtual UserVm User { get; set; }

        public virtual ParameterVm Parameter { get; set; }

        public virtual BranchVm? Branch { get; set; }

        public virtual UserVm? VerifiedBy { get; set; }

        public virtual UserVm? ValidatedBy { get; set; }


    }
}
