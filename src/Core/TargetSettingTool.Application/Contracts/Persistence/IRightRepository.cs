using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface IRightRepository : IAsyncRepository<Right>
    {
        public Task<Right> IsRightExist(string right);
    }
}
