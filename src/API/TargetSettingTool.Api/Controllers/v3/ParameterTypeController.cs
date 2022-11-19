using MediatR;

using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Features.ParameterTypes.Commands.CreateParameterType;
using TargetSettingTool.Application.Features.ParameterTypes.Commands.UpdateParameterType;
using TargetSettingTool.Application.Features.ParameterTypes.Queries.GetParameterTypeById;
using TargetSettingTool.Application.Features.ParameterTypes.Queries.GetParameterTypeByName;
using TargetSettingTool.Application.Features.ParameterTypes.Commands.DeleteParameterType;
using TargetSettingTool.Application.Features.ParameterTypes.Queries.GetAllParamterTypes;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ParameterTypeController : Controller
    {
        private readonly ILogger<ParameterTypeController> _logger;
        private readonly IMediator _mediator;
        public ParameterTypeController(ILogger<ParameterTypeController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateParameterType")]
        public async Task<ActionResult> CreateParamterType(CreateParameterTypeCommand createParameterTypeCommand)
        {
            _logger.LogInformation("Create ParameterType Command Initiated");
            var response = await _mediator.Send(createParameterTypeCommand);
            _logger.LogInformation("Create ParameterType Command Completed");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllParameterTypes")]
        public async Task<ActionResult> GetAllParameterTypes()
        {
            _logger.LogInformation("GetAllParameterTypes Query Initiated");
            var response = await _mediator.Send(new GetAllParameterTypesQuery());
            _logger.LogInformation("GetAllParameterTypes Query Initiated");
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateParameterType")]
        public async Task<ActionResult> UpdateRole(UpdateParameterTypeCommand updateParameterTypeCommand)
        {
            _logger.LogInformation("UpdateParameterType Command Initiated");
            var response = await _mediator.Send(updateParameterTypeCommand);
            _logger.LogInformation("USpdateParameterType Command Completed");
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteParameterType")]
        public async Task<ActionResult> DeleteParameterType(Guid id, Guid logInUser)
        {
            _logger.LogInformation("Delete ParameterType Command Initiated");
            var response = await _mediator.Send(new DeleteParameterTypeCommand() { Id = id, LoggedInUserId = logInUser });
            _logger.LogInformation("Delete ParameterType Command Completed");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetParameterTypeById")]
        public async Task<IActionResult> GetParameterTypeById(Guid id)
        {
            _logger.LogInformation("GetParameterTypeBydId Query Initiated");
            var response = await _mediator.Send(new GetParameterTypeByIdQuery() { Id = id });
            _logger.LogInformation("GetParameterTypeBydId Query Completed");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetParameterTypeByName")] 
        public async Task<ActionResult> GetParameterTypeByName(Guid id, string name)
        {
            _logger.LogInformation("GetParameterTypeByName Query Initiated");
            var response = await _mediator.Send(new GetParameterTypeByNameQuery() { Id = id, Name = name });
            _logger.LogInformation("GetParameterTypeByName Query Completed");
            return Ok(response);
        }


    }
}
