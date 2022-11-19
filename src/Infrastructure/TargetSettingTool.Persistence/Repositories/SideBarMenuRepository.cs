using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Persistence.Repositories
{
    public class SideBarMenuRepository : BaseRepository<Menu>, ISideBarMenuRepository
    {
        List<SideBarMenu> SideBar;
        public SideBarMenuRepository(ApplicationDbContext dbContext, ILogger<Menu> logger) : base(dbContext, logger)
        {
            SideBar = new List<SideBarMenu>();
        }

        public async Task<List<SideBarMenu>> GetSideBarList()
        {
            var Parent = await _dbContext.TblMenu.Where(x => x.ParentMenuId == null).ToListAsync();
            var childs = await _dbContext.TblMenu.Where(x => x.ParentMenuId != null).ToListAsync();
            for (int i = Parent.Count-1; i >= 0; i--)
            {
                List<Menu> subMenu = childs.Where(x => x.ParentMenuId == Parent[i].Id).ToList();
                SideBar.Add(new SideBarMenu
                {
                    Menu = Parent[i],
                    SubMenus = subMenu
                });
            }
            return SideBar;
        }
    }
}
