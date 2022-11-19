﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface ISideBarMenuRepository : IAsyncRepository<Menu>
    {
        public Task<List<SideBarMenu>> GetSideBarList();
    }
}
