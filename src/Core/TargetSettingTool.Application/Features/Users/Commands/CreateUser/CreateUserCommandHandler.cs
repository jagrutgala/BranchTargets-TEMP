using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;
using TargetSettingTool.Application.Helper;
using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;
using System.Net.Mail;
using System.Net;
using TargetSettingTool.Application.Models.Mail;

namespace TargetSettingTool.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response<Guid>>
    {
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserBranchRepository _userBranchRepository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(ILogger<CreateUserCommandHandler> logger, IUserRepository userRepository, IUserBranchRepository userBranchRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userBranchRepository = userBranchRepository;
            _mapper = mapper;
        }
        public async Task<Response<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            User userToAdd = _mapper.Map<User>(request);
            userToAdd.CreatedDate = DateTime.Now;
            userToAdd.CreatedBy = request.LoggedInUserId;
            string password = PasswordUtilities.GeneratePassword(8);
            userToAdd.Password = PasswordUtilities.HashPassword(password);
            User user = await _userRepository.AddAsync(userToAdd);
            // UserBranches Add
            List<UserBranch> userBranchesToAdd = new List<UserBranch>();
            if (request.Branches != null && request.Branches.Count()>0)
            {
                foreach (Guid branchId in request.Branches)
                {
                    userBranchesToAdd.Add(new UserBranch { UserId = user.Id, BranchId = branchId, CreatedBy=request.LoggedInUserId, CreatedDate=DateTime.Now });
                } 
                List<UserBranch> userBranches = await _userBranchRepository.AddUserBranchRangeAsync(userBranchesToAdd);
            }
            var response = new Response<Guid>(user.Id, "Created User");
            user = await _userRepository.GetUserByIdAsync(user.Id);
            var mail = new MailMessage()
            {
                From = new MailAddress("malaysatiya19@gmail.com"),
                Body = $"Dear {user.Name}, <br> <br> Your Registration to Target Setting Tool has been done successfully.  <br><br> Please find your Login Credentials below : <br>Employee Code : {user.EmployeeCode}<br>Password : {password}<br><br><b>Note* : Don't share your Employee Code And Password with anyone.</b><br><br> Regards, <br>TargetSettingTool Team.",
                Subject = "Login Credentials",
                IsBodyHtml = true,
            };
            mail.To.Add(user.Email);
            mail.Bcc.Add(new MailAddress("sonali.pardeshi@neosoftmail.com"));
            try
            {
                SendMail(mail);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error While Sending Login Credentials: {ex.Message}");
            }
            _logger.LogInformation("Handler Completed");
            return response;
        }
        private void SendMail(MailMessage mail)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("malaysatiya19@gmail.com", "fgdkahjoqutcunoa");
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
