using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using TargetSettingTool.UI.Models.ParameterType;

namespace TargetSettingTool.UI.Models.Parameter
{
    public class ParameterVm
    {
        public Guid Id { get; set; }
        [DisplayName("Parameter Type")]
        public Guid ParameterTypeId { get; set; }

        [DisplayName("Target Amount")]
        public double TargetAmount { get; set; }

        [DisplayName("Start Date")]
        public DateTime StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime EndDate { get; set; }

        [DisplayName("Financial Year")]
        public string FinancialYear { get; set; }

        public ParameterTypeVm ParameterType { get; set; }
    }
}
