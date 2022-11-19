using TargetSettingTool.UI.Models.UserParameter;

namespace TargetSettingTool.UI.Models.BranchTarget
{
    public class BranchTargetVm
    {
        public Guid UserParameterId { get; set; }

        public double AchievedTargetAmount { get; set; }

        public UserParameterVm UserParameter { get; set; }
    }
}
