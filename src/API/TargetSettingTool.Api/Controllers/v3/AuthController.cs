using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Models.Auth;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService; 
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost]
        [Route("UserLogin")]
        public async Task<ActionResult> UserLogin(UserLoginVm userLoginViewModel)
        {
            _logger.LogInformation("User Login Initiated");
            try
            {
                Response<AuthResponse> response = await _authService.FindUserByEmployeeCodeAsync(userLoginViewModel.EmployeeCode, userLoginViewModel.Password);
                if (response != null)
                {
                    _logger.LogInformation("User Login Completed");
                    return Ok(response);
                }
                return BadRequest(new Response<AuthResponse>("Failed To Login"));

            }
            catch(Exception e)
            {
                return BadRequest(new Response<AuthResponse>(e.Message));
            }
        }

        [HttpPost]
        [Route("ForgetPassword")]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordVm forgetPassword)
        {
            _logger.LogInformation("Forget Password Initiated");
            var response = await _authService.ResetPassword(forgetPassword.EmployeeCode);
            if (response != null)
            {
                _logger.LogInformation("Forget Password Completed");
                return Ok(response);
            }
            return BadRequest(new Response<string> { Succeeded = false, Message = "Failed To Reset Password" });
        }
    }
}
