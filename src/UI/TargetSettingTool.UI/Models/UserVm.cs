using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.UI.Models.Branch;

namespace TargetSettingTool.UI.Models
{
    public class UserVm
    {
        public Guid Id { get; set; }

        [RegularExpression("^[A-Za-z0-9 ]+$", ErrorMessage = "Name can only contain alphabets and numbers.")]
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required, RegularExpression("^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4})*$", ErrorMessage = "Enter a valid Email Address.")]
        [Remote("IsEmailExist", "User", AdditionalFields ="Id", HttpMethod = "POST", ErrorMessage = "Email already exists.")]
        [MaxLength(100, ErrorMessage = "Email cannot be larger than 100 characters")]
        [DisplayName("Email")]
        public string Email { get; set; }

        [RegularExpression("^[789][0-9]{9}$", ErrorMessage = "Enter a valid Phone Number.")]
        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Remote("IsEmployeeCodeExist", "User", AdditionalFields ="Id", HttpMethod = "POST", ErrorMessage = "EmployeeCode already exists.")]
        [Required]
        [DisplayName("Employee Code")]
        public string EmployeeCode { get; set; }

        [Required]
        [DisplayName("Role")]
        public Guid RoleId { get; set; }

        [Required]
        [DisplayName("Right")]
        public Guid? RightId { get; set; }

        public Guid? LoggedInUserId { get; set; }


        public virtual RoleVm Role { get; set; }

        public virtual RightVm? Right { get; set; }

        [Required]
        [DisplayName("Branches")]
        public virtual List<Guid>? Branches { get; set; }
        public virtual ICollection<UserBranchVm>? UserBranches { get; set; }
    }

    public class UserBranchVm
    {
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public virtual BranchVm Branch { get; set; }
    }

    public class RoleVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class RightVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
