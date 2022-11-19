using System.ComponentModel.DataAnnotations.Schema;
using TargetSettingTool.Application.Features.Branches.Queries.Common;

namespace TargetSettingTool.Application.Features.Users.Queries.Common
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string EmployeeCode { get; set; }
        public Guid RoleId { get; set; }
        public Guid? RightId { get; set; }
        public virtual RoleVm Role { get; set; }
        public virtual RightVm? Right { get; set; }
        public virtual List<Guid>? Branches { get; set; }
        public virtual ICollection<UserBranchVm>? UserBranches { get; set; }
    }

    public class UserBranchVm
    {
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public virtual BranchDto Branch { get; set; }
    }

    public class RightVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class RoleVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
