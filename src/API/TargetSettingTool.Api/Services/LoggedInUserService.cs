using TargetSettingTool.Application.Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace TargetSettingTool.Api.Services
{
    public class LoggedInUserService:ILoggedInUserService
    {
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public string UserId { get; }
    }
}
