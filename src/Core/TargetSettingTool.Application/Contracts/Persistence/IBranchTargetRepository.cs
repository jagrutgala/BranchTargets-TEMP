using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface IBranchTargetRepository: IAsyncRepository<BranchTarget>
    {
        public Task<IEnumerable<BranchTarget>> GetBranchTargetByBranchIdAndStartDate (Guid branchId, DateTime startDate);
    }
}
