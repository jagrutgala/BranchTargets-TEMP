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
    public class ParameterTypeRepository : BaseRepository<ParameterType>, IParameterTypeRepository
    {
        private readonly ILogger _logger;
        protected readonly ApplicationDbContext _dbContext;
        public ParameterTypeRepository(ApplicationDbContext dbContext, ILogger<ParameterType> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<ParameterType> GetParameterTypeByNameAsync(string name)
        {
            var result = await _dbContext.MstParameterTypes.Where(x => x.Name == name && x.IsDeleted == false).FirstOrDefaultAsync();
            if (result != null)
            {
                return result;
            }
            return null;

        }
    }
}
