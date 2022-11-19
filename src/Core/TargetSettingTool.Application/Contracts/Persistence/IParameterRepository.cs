using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface IParameterRepository : IAsyncRepository<Parameter>
    {
        public Task<IEnumerable<Parameter>> AddRangeAsync(List<Parameter> parameters);
        public Task<IEnumerable<Parameter>> GetAllParametersAsync();
        public Task<bool> GetParameterByTypeAsync(string name);
    }
}
