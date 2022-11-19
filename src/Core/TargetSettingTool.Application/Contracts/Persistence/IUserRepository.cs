using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TargetSettingTool.Application.Features.Users.Queries.Common;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface IUserRepository:IAsyncRepository<User>
    {
        public Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<User> GetUserByEmployeeCodeAsync(string employeeCode);
        public Task<User> GetUserByIdAsync(Guid id);
        public Task<List<User>> GetUsersByRoleName(string role);

    }
}
