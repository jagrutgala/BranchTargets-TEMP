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
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        protected readonly ApplicationDbContext _dbContext;
        private readonly ILogger<Role> _logger;
        public RoleRepository(ApplicationDbContext dbContext, ILogger<Role> logger) : base(dbContext, logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }
        public async Task<Role> GetRoleByNameAsync(string name)
        {
            var result =  await _dbContext.MstRoles.Where(x => x.Name == name && x.IsDeleted == false).FirstOrDefaultAsync();
            if (result != null) 
            {
                return result;
            }
            return null;
        }
    }
}
