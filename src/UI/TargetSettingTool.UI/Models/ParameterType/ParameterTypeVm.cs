using System.ComponentModel;

namespace TargetSettingTool.UI.Models.ParameterType
{
    public class ParameterTypeVm
    {
        public Guid Id { get; set; }

        [DisplayName("Parameter Type")]
        public string Name { get; set; }
    }
}
