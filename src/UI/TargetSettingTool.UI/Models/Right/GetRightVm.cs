using System.ComponentModel;

namespace TargetSettingTool.UI.Models.Right
{
    public class GetRightVM
    {
        public Guid Id { get; set; }
        [DisplayName("Right")]
        public string Name { get; set; }
    }
}
