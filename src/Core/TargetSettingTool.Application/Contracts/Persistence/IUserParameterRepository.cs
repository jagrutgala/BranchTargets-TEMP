using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface IUserParameterRepository : IAsyncRepository<UserParameter>
    {
        public Task<IEnumerable<UserParameter>> GetAllUserParametersByBranchId(Guid branchId);

    }
}
