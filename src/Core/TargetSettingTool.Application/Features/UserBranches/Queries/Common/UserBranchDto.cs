using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Features.Users.Queries.Common;

namespace TargetSettingTool.Application.Features.UserBranches.Queries.Common
{
    public class UserBranchDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }

        public virtual UserDto User { get; set; }
        public virtual BranchDto Branch { get; set; }
    }
}
