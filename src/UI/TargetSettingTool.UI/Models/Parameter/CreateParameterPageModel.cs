using System.ComponentModel.DataAnnotations;

namespace TargetSettingTool.UI.Models.Parameter
{
    public class CreateParameterPageModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string FinancialYear { get; set; }
    }
}
