using System.ComponentModel.DataAnnotations;

namespace TargetSettingTool.UI.Models.SideBar
{
    public class Menu
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentMenuId { get; set; }

        public string? Url { get; set; }

        public bool IsDeleted { get; set; }

        public Guid? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
