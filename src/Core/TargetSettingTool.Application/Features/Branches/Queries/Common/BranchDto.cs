
using TargetSettingTool.Application.Features.Users.Queries.Common;

namespace TargetSettingTool.Application.Features.Branches.Queries.Common
{
    public class BranchDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid RegionId { get; set; }

        public virtual UserDto Region { get; set; }
    }
}
