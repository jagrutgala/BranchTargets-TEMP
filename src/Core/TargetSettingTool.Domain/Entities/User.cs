using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Common;

namespace TargetSettingTool.Domain.Entities
{
    public class User : CommonAuditProperties
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeNumber { get; set; }

        [MaxLength(100, ErrorMessage = "Name cannot be larger than 100 characters")]
        public string Name { get; set; }

        [MaxLength(100, ErrorMessage = "Email cannot be larger than 100 characters")]
        public string Email { get; set; }

        public string Password { get; set; }

        [MaxLength(100, ErrorMessage = "Phone Number cannot be larger than 100 characters")]
        public string PhoneNumber { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string EmployeeCode { get; set; }

        public Guid RoleId { get; set; }

        public Guid? RightId { get; set; }
        public Guid? ParentUserId { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; }

        [ForeignKey("RightId")]
        public virtual Right? Right { get; set; }
        public virtual ICollection<UserBranch>? UserBranches { get; set; }

        

    }
}
