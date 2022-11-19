using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TargetSettingTool.UI.Models.Parameter
{
    public class UpdateParameterVm
    {
        public Guid Id { get; set; }
        [Required]
        [DisplayName("Parameter Type")]
        public Guid ParameterTypeId { get; set; }

        [Required]
        [DisplayName("Target Amount")]
        public double TargetAmount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }
        [DisplayName("Financial Year")]
        public string FinancialYear { get; set; }

    }
}
