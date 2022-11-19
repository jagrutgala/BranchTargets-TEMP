using System.ComponentModel;

namespace TargetSettingTool.UI.Models.Branch
{
    public class BranchVm
    {
        public Guid Id { get; set; }
        [DisplayName("Region")]
        public Guid RegionId { get; set; }
        [DisplayName("Branch")]
        public string Name { get; set; }

        public virtual UserVm? Region { get; set; }
    }
}
