using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Helper;
using TargetSettingTool.Application.Models.Auth;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        public AuthService(IUserRepository userRepository, IMapper mapper, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<AuthResponse>> FindUserByEmployeeCodeAsync(string employeeCode, string password)
        {
            User user = await _userRepository.GetUserByEmployeeCodeAsync(employeeCode);
            _logger.LogInformation("Find User By EmployeeCode Service Initiated");
            if (user == null)
            {

                throw new Exception("Failed to find user with this credentials");
            }
            else
            {
                var passwordHash = PasswordUtilities.HashPassword(password);
                if (passwordHash == user.Password)
                {
                    _logger.LogInformation("User Found");
                    var loginUser = _mapper.Map<AuthResponse>(user);
                    return new Response<AuthResponse>(loginUser, "Login Successfull");
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Response<string>> ResetPassword(string employeeCode)
        {
            _logger.LogInformation("Reset Password Initiated");
            User user = await _userRepository.GetUserByEmployeeCodeAsync(employeeCode);
            if (user != null)
            {
                _logger.LogInformation("User Found! Generating a new password");
                var password = PasswordUtilities.GeneratePassword(8);
                user.Password = PasswordUtilities.HashPassword(password);
                user.LastModifiedBy = user.Id;
                user.LastModifiedDate = DateTime.Now;
                await _userRepository.UpdateAsync(user);
                _logger.LogInformation("Reset Password Successfull");
                SendResetPasswordEmail(user, password);
                return new Response<string> { Succeeded = true, Message = user.Email };
            }
            _logger.LogInformation("User Not Found! Failed To Reset Password");
            return null;
        }

        private void SendResetPasswordEmail(User user, string password)
        {

            using (MailMessage msg = new MailMessage("malaysatiya19@gmail.com", user.Email))
            {
                msg.Subject = "Password Reset Successfully!!";
                string body = $"Dear User, <br/><br/>Your Password has been reset successfully.<br/>\r\n  Use following credentials to Login.<br/>\r\nEmployeeCode : {user.EmployeeCode} <br/>\r\nPassword : {password}<br /><br />Thanks & Regards, <br/> Target Setting Tool Team.";
                msg.Body = body;
                msg.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("malaysatiya19@gmail.com", "fgdkahjoqutcunoa");
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(msg);
            }
        }

    }
}
