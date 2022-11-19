using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Persistence.Repositories
{
    public class ParameterRepository : BaseRepository<Parameter>, IParameterRepository
    {
        public ParameterRepository(ApplicationDbContext dbContext, ILogger<Parameter> logger) : base(dbContext, logger)
        {

        }

        public async Task<IEnumerable<Parameter>> AddRangeAsync(List<Parameter> parameters)
        {
            await _dbContext.MstParameters.AddRangeAsync(parameters);
            await _dbContext.SaveChangesAsync();
            return parameters;
        }

        public async Task<IEnumerable<Parameter>> GetAllParametersAsync()
        {
            return await _dbContext.MstParameters
                .Include(x => x.ParameterType)
                .ToListAsync();
        }

        public async Task<bool> GetParameterByTypeAsync(string name)
        {
            var parameterType = await _dbContext.MstParameters.Where(x => x.ParameterType.Name == name).FirstOrDefaultAsync();
            if(parameterType != null)
            {
                return true;
            }
            return false;
        }
    }
}
