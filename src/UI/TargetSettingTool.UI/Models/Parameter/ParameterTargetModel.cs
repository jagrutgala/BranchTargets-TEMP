using System.ComponentModel.DataAnnotations;

namespace TargetSettingTool.UI.Models.Parameter
{
    public class ParameterTargetModel
    {
        [Required]
        public string TargetAmount { get; set; }

        [Required]
        public string ParameterTypeId { get; set; }
    }
}
