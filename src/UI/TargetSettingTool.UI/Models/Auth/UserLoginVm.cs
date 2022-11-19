using System.ComponentModel.DataAnnotations;

namespace TargetSettingTool.UI.Models.Auth
{
    public class UserLoginVm
    {
        [Required]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "Please Enter A Valid Employee Code")]
        public string EmployeeCode { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(4)]
        public string CaptchaCode { get; set; }
    }
}
