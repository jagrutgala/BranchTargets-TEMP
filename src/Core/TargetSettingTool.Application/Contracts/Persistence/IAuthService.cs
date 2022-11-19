using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TargetSettingTool.Application.Models.Auth;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface IAuthService
    {
        public Task<Response<AuthResponse>> FindUserByEmployeeCodeAsync(string employeeCode, string password);
        public Task<Response<string>> ResetPassword(string employeeCode);
    }
}
