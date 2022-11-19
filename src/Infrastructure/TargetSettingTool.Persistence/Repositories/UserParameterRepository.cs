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
    public class UserParameterRepository : BaseRepository<UserParameter>, IUserParameterRepository
    {
        public UserParameterRepository(ApplicationDbContext dbContext, ILogger<UserParameter> logger) : base(dbContext, logger)
        {
        }

        public async Task<IEnumerable<UserParameter>> GetAllUserParametersByBranchId(Guid branchId)
        {
            return await _dbContext.TblUserParameters
                .Where(x => x.BranchId == branchId)
                .Include(x => x.User)
                .Include(x => x.Branch)
                .Include(x => x.VerifiedBy)
                .Include(x => x.ValidatedBy)
                .Include(x => x.Parameter)
                .ThenInclude(x => x.ParameterType)
                .ToListAsync();
        }
    }
}
