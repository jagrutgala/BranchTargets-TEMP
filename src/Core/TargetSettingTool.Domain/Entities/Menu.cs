using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetSettingTool.Domain.Entities
{
    public class Menu
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100, ErrorMessage = "Menu Name cannot be larger than 100 characters")]
        public string Name { get; set; }

        public Guid? ParentMenuId { get; set; }

        public string? Url { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

    }
}
