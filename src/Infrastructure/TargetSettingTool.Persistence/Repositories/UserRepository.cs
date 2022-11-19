using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Features.Users.Queries.Common;
using TargetSettingTool.Application.Models.Mail;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext, ILogger<User> logger) : base(dbContext, logger)
        {
        }
        public async Task<User> GetUserByIdAsync(Guid id)
        {
            User user = await _dbContext.Set<User>()
                .Include(u => u.Role)
                .Include(u => u.Right)
                .Include(x=>x.UserBranches)
                .ThenInclude(x=>x.Branch)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Set<User>().Where(x => x.IsDeleted == false).Include(u=>u.Role).Include(u => u.Right).OrderByDescending(u => u.CreatedDate).ToListAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Email == email && u.IsDeleted==false);
        }

        public async Task<User> GetUserByEmployeeCodeAsync(string employeeCode)
        {
            return await _dbContext.Set<User>().Include(u => u.Role).Include(u => u.Right).FirstOrDefaultAsync(u => u.EmployeeCode == employeeCode && u.IsDeleted==false);
        }

        public async Task<List<User>> GetUsersByRoleName(string role)
        {
            return await _dbContext.MstUsers
                .Include(x => x.Role)
                .Where(x => x.Role.Name == role)
                .Where(x => x.Right.Name == "Maker")
                .ToListAsync();
        }
    }
}
