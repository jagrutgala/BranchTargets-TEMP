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
    public class RightRepository : BaseRepository<Right>, IRightRepository
    {
        private readonly ApplicationDbContext _DbContext;
        public RightRepository(ApplicationDbContext dbContext, ILogger<Right> logger) : base(dbContext, logger)
        {
            _DbContext = dbContext;
        }

        public async Task<Right> IsRightExist(string right)
        {
            var result = await _DbContext.MstRights.Where(x => x.Name == right && x.IsDeleted == false).FirstOrDefaultAsync();
            if (result != null) { 
                return result;
            }
            return null;
        }
    }
}
