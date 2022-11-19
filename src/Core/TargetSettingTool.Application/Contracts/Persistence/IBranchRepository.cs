using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface IBranchRepository : IAsyncRepository<Branch>
    {
        public Task<IEnumerable<Branch>> GetAllAsync();
        public Task<Branch> GetByNameAsync(string name);
    }
}
