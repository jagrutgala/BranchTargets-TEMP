using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Features.Users.Commands.CreateUser;
using TargetSettingTool.Application.Features.Users.Commands.DeleteUser;
using TargetSettingTool.Application.Features.Users.Commands.UpdateUser;
using TargetSettingTool.Application.Features.Users.Queries.GetAllUsers;
using TargetSettingTool.Application.Features.Users.Queries.GetUserById;
using TargetSettingTool.Application.Features.Users.Queries.GetUsersWhereRoleIsRegion;
using TargetSettingTool.Application.Features.Users.Queries.IsEmailExist;
using TargetSettingTool.Application.Features.Users.Queries.IsEmployeeCodeExist;
using TargetSettingTool.Application.Models.User;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateUserCommand createUserCommand)
        {
            _logger.LogInformation("CreateUser Command Initiated");
            var response = await _mediator.Send(createUserCommand);
            _logger.LogInformation("CreateUser Command Completed");
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            _logger.LogInformation("UpdateUser Command Initiated");
            var response = await _mediator.Send(updateUserCommand);
            _logger.LogInformation("UpdateUser Command Completed");
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetAllUsers()
        {
            _logger.LogInformation("GetAllUsers Query Initiated");
            var response = await _mediator.Send(new GetAllUsersQuery());
            _logger.LogInformation("GetAllUsers Query Initiated");
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetUserById(Guid id)
        {
            _logger.LogInformation("GetUserById Query Initiated");
            var response = await _mediator.Send(new GetUserByIdQuery() { Id = id });
            _logger.LogInformation("GetUserById Query Initiated");
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUser(Guid id, Guid loggedInUserId)
        {
            _logger.LogInformation("DeleteUser Command Initiated");
            var response = await _mediator.Send(new DeleteUserCommand() { Id = id, LoggedInUserId = loggedInUserId });
            _logger.LogInformation("DeleteUser Command Completed");
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> IsEmailExist(string email, Guid id)
        {
            _logger.LogInformation("IsEmailExist Query Initiated");
            var response = await _mediator.Send(new IsEmailExistQuery() { Id = id, Email = email });
            _logger.LogInformation("IsEmailExist Query Initiated");
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> IsEmployeeCodeExist(string employeeCode, Guid id)
        {
            _logger.LogInformation("IsEmployeeCodeExist Query Initiated");
            var response = await _mediator.Send(new IsEmployeeCodeExistQuery() { Id = id, EmployeeCode = employeeCode });
            _logger.LogInformation("IsEmployeeCodeExist Query Initiated");
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetUsersWhereRoleIsRegion()
        {
            _logger.LogInformation("GetUsersWhereRoleIsRegion Query Initiated");
            var response = await _mediator.Send(new GetUsersWhereRoleIsRegionQuery());
            _logger.LogInformation("GetUsersWhereRoleIsRegion Query Initiated");
            return Ok(response);
        }
        //[HttpGet]
        //public async Task<ActionResult> UserLogin(LoginViewModel userLogin)
        //{
        //    _logger.LogInformation("User Login Initiated");

        //}

    }
}
