using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TargetSettingTool.Application.Features.Roles.Commands.CreateRole;
using TargetSettingTool.Application.Features.Roles.Commands.DeleteRole;
using TargetSettingTool.Application.Features.Roles.Commands.UpdateRole;
using TargetSettingTool.Application.Features.Roles.Queries;
using TargetSettingTool.Application.Features.Roles.Queries.GetAllRoles;
using TargetSettingTool.Application.Features.Roles.Queries.GetRoleById;
using TargetSettingTool.Application.Features.Roles.Queries.GetRoleByName;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<ParameterController> _logger;
        private readonly IMediator _mediator;
        public RoleController(ILogger<ParameterController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateRole")]
        public async Task<ActionResult> CreateRole(CreateRoleCommand createRoleCommand)
        {
            _logger.LogInformation("CreateRole Command Initiated");
            var response = await _mediator.Send(createRoleCommand);
            _logger.LogInformation("CreateRole Command Completed");
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateRole")]
        public async Task<ActionResult> UpdateRole(UpdateRoleCommand updateRoleCommand)
        {
            _logger.LogInformation("UpdateRole Command Initiated");
            var response = await _mediator.Send(updateRoleCommand);
            _logger.LogInformation("UpdateRole Command Completed");
            return Ok(response);
        }

        [HttpDelete("DeleteRole")]
        public async Task<ActionResult> DeleteRole(Guid id, Guid logInUser)
        {
            _logger.LogInformation("Delete Role Command Initiated");
            var response = await _mediator.Send(new DeleteRoleCommand() { Id = id, LoggedInUserId = logInUser});
            _logger.LogInformation("Delete Role Command Completed");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<ActionResult> GetAllRoles()
        {
            _logger.LogInformation("GetAlllRoles Query Initiated");
            var response = await _mediator.Send(new GetAllRolesQuery());
            _logger.LogInformation("GetAlllRoles Query Initiated");
            return Ok(response);
        }

        [HttpGet("GetRoleById")]
        public async Task<ActionResult> GetRoleById(Guid id)
        {
            _logger.LogInformation("GetRoleById Query Initiated");
            var response = await _mediator.Send(new GetRoleByIdQuery() { Id = id });
            _logger.LogInformation("GetRoleById Query Completed");
            return Ok(response);
        }

        [HttpGet("GetRoleByName")]
        public async Task<ActionResult> GetRoleByName(Guid id, string name)
        {
            _logger.LogInformation("GetRoleByName Query Initiated");
            var response = await _mediator.Send(new GetRoleByNameQuery() { Id = id, Name = name });
            _logger.LogInformation("GetRoleByName Query Completed");
            return Ok(response);
        }
    }
}
