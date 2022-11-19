using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Features.Parameters.Commands.CreateParameter;
using TargetSettingTool.Application.Features.Parameters.Commands.CreateParameters;
using TargetSettingTool.Application.Features.Parameters.Commands.DeleteParameter;
using TargetSettingTool.Application.Features.Parameters.Commands.UpdateParameter;
using TargetSettingTool.Application.Features.Parameters.Queries.GetAllParameters;
using TargetSettingTool.Application.Features.Parameters.Queries.GetParameterByType;
using TargetSettingTool.Application.Features.Parameters.Queries.GetParametersById;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParameterController : ControllerBase
    {
        private readonly ILogger<ParameterController> _logger;
        private readonly IMediator _mediator;
        public ParameterController(ILogger<ParameterController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateParameter")]
        public async Task<ActionResult> CreateParameter(CreateParameterCommand createParameterCommand)
        {
            _logger.LogInformation("Create Parameter Command Initiated");
            var response = await _mediator.Send(createParameterCommand);
            _logger.LogInformation("Create Parameter Command Completed");
            return Ok(response);
        }

        [HttpPost]
        [Route("CreateParameters")]
        public async Task<ActionResult> CreateParameters(CreateParametersCommand createParametersCommand)
        {
            _logger.LogInformation("Create Parameter Command Initiated");
            var response = await _mediator.Send(createParametersCommand);
            _logger.LogInformation("Create Parameter Command Completed");
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateParameter")]
        public async Task<ActionResult> UpdateParameter(UpdateParameterCommand updateParameterCommand)
        {
            _logger.LogInformation("Update Parameter Command Initiated");
            var response = await _mediator.Send(updateParameterCommand);
            _logger.LogInformation("Update Parameter Command Completed");
            return Ok(response);
        }

        [HttpDelete("DeleteParameter")]
        public async Task<ActionResult> DeleteParameter(Guid id, Guid logInUser)
        {
            _logger.LogInformation("Delete Parameter Command Initiated");
            var response = await _mediator.Send(new DeleteParameterCommand() { Id = id, LoggedInUserId = logInUser});
            _logger.LogInformation("Delete Parameter Command Completed");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllParameters")]
        public async Task<ActionResult> GetAllParameters()
        {
            _logger.LogInformation("GetAlllParameters Query Initiated");
            var response = await _mediator.Send(new GetAllParametersQuery());
            _logger.LogInformation("GetAlllParameters Query Initiated");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetParameterById")]
        public async Task<ActionResult> GetParameterById(Guid id)
        {
            _logger.LogInformation("GetParameterById Query Initiated");
            var response = await _mediator.Send(new GetParameterByIdQuery() { Id = id });
            _logger.LogInformation("GetParameterById Query Completed");
            return Ok(response);
        }

        [HttpGet("GetParameterByType")]
        public async Task<ActionResult> GetParameterByType(string name)
        {
            _logger.LogInformation("GetParameterByType Query Initiated");
            var response = await _mediator.Send(new GetParameterByTypeQuery() { Name = name });
            _logger.LogInformation("GetParameterByType Query Completed");
            return Ok(response);
        }
    }
}
