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
    public class BranchRepository : BaseRepository<Branch>, IBranchRepository
    {
        public BranchRepository(ApplicationDbContext dbContext, ILogger<Branch> logger) : base(dbContext, logger)
        {
        }

        public async Task<IEnumerable<Branch>> GetAllAsync()
        {
            return await _dbContext.MstBranches
                .Include(u=>u.Region)
                .ToListAsync();
        }

        public async Task<Branch> GetByNameAsync(string name)
        {
            return _dbContext.MstBranches
                .FirstOrDefault(x => x.Name == name);
        }
    }
}
