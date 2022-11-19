using System.ComponentModel.DataAnnotations;

namespace TargetSettingTool.UI.Models.Auth
{
    public class ForgotPasswordVm
    {
        [Required]
        public string EmployeeCode { get; set; }
    }
}
