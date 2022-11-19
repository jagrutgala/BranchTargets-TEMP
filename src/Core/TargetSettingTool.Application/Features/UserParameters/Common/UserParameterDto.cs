using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Features.Parameters.Queries.GetAllParameters;
using TargetSettingTool.Application.Features.Users.Queries.Common;

namespace TargetSettingTool.Application.Features.UserParameters.Common
{
    public class UserParameterDto
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

        public virtual UserDto User { get; set; }

        public virtual ParameterDto Parameter { get; set; }

        public virtual BranchDto? Branch { get; set; }

        public virtual UserDto? VerifiedBy { get; set; }

        public virtual UserDto? ValidatedBy { get; set; }

    }
}
