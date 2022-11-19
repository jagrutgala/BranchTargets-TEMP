using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Persistence.Repositories
{
    public class UserBranchRepositry : BaseRepository<UserBranch>, IUserBranchRepository
    {
        public UserBranchRepositry(ApplicationDbContext dbContext, ILogger<UserBranch> logger) : base(dbContext, logger)
        {
        }

        public async Task<List<UserBranch>> AddUserBranchRangeAsync(List<UserBranch> userBranches)
        {
            await _dbContext.TblUserBranches.AddRangeAsync(userBranches);
            await _dbContext.SaveChangesAsync();
            return userBranches;
        }

        public async Task<List<UserBranch>> GetBranchesByUserIdAsync(Guid userId)
        {
            return await _dbContext.TblUserBranches
                .Where(b => b.UserId == userId)
                .Include(x => x.User)
                .Include(x => x.Branch)
                .ToListAsync();
        }

        public async Task<IEnumerable<Branch>> GetUniqueBranches()
        {
            return (IEnumerable<Branch>)await _dbContext.TblUserBranches
                .Include(x => x.Branch)
                .Select(x => x.Branch)
                .Distinct()
                .ToListAsync();
        }

        public async Task<bool> RemoveUserBranchRangeAsync(List<UserBranch> userBranches)
        {
            _dbContext.TblUserBranches.RemoveRange(userBranches);
            return await _dbContext.SaveChangesAsync()>=0;
        }
    }
}
