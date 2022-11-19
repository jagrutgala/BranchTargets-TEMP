using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TargetSettingTool.Domain.Entities
{
    public class SideBarMenu
    {
        public Menu Menu { get; set; }

        public List<Menu> SubMenus { get; set; } 
    }
}
