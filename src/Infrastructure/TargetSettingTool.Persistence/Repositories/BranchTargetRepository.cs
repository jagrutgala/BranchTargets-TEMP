using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Persistence.Repositories
{
    public class BranchTargetRepository : BaseRepository<BranchTarget>, IBranchTargetRepository
    {
        public BranchTargetRepository(ApplicationDbContext dbContext, ILogger<BranchTarget> logger) : base(dbContext, logger)
        {
        }

        public async Task<IEnumerable<BranchTarget>> GetBranchTargetByBranchIdAndStartDate(Guid branchId, DateTime startDate)
        {
            return await _dbContext.TblBranchTargets
                .Include(x => x.UserParameter)
                .ThenInclude(x => x.Parameter)
                .Where(x => x.UserParameter.BranchId == branchId)
                .Where(x => x.UserParameter.Parameter.StartDate == startDate)
                .ToListAsync();
        }
    }
}
