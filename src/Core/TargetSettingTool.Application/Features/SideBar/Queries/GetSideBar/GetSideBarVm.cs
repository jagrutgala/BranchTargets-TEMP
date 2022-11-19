using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.SideBar.Queries.GetSideBar
{
    public class GetSideBarVm
    {
        public Menu Menu { get; set; }

        public List<Menu> SubMenus { get; set; }

    }
}
