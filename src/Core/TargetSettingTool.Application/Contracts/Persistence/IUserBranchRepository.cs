using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface IUserBranchRepository : IAsyncRepository<UserBranch>
    {
        public Task<List<UserBranch>> AddUserBranchRangeAsync(List<UserBranch> userBranches);
        public Task<bool> RemoveUserBranchRangeAsync(List<UserBranch> userBranches);
        public Task<List<UserBranch>> GetBranchesByUserIdAsync(Guid id);
        public Task<IEnumerable<Branch>> GetUniqueBranches();
    }
}
