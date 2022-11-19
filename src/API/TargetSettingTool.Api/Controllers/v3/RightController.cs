using MediatR;

using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Features.Rights.Commands.CreateRight;
using TargetSettingTool.Application.Features.Rights.Commands.DeleteRight;
using TargetSettingTool.Application.Features.Rights.Commands.UpdateRight;
using TargetSettingTool.Application.Features.Rights.Queries.GetAllRights;
using TargetSettingTool.Application.Features.Rights.Queries.GetRightById;
using TargetSettingTool.Application.Features.Rights.Queries.GetRightByName;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class RightController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<RightController> _logger;

        public RightController(IMediator mediator,ILogger<RightController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllRights")]
        public async Task<ActionResult> GetAllRights()
        {
            _logger.LogInformation("GetAllRights In Controller Init");
            var res = await _mediator.Send(new GetAllRightsQuery());
            _logger.LogInformation("GetAllRights In Controller finished");
            return Ok(res);
        }

        [HttpPost]
        [Route("CreateRight")]
        public async Task<ActionResult> CreateRight(CreateRightCommand createRightCommand) {
            _logger.LogInformation("Create Right in Controller Init");
            var res = await _mediator.Send(createRightCommand);
            _logger.LogInformation("Create Right in Controller Finished");
            return Ok(res);
        }
        [HttpDelete]
        [Route("DeleteRight")]
        public async Task<ActionResult> DeleteRight(Guid id,Guid loggedIn) {
            _logger.LogInformation("Delete Right in Controller Init");
            var res = await _mediator.Send(new DeleteRightCommand() { Id = id,LoggedIn = loggedIn});
            _logger.LogInformation("Delete Right in Controller finished");
            return Ok(res);
        }

        [HttpPut]
        [Route("UpdateRight")]
        public async Task<ActionResult> UpdateRight(UpdateRightCommand updateRightCommand) {
            _logger.LogInformation("Update Right in Controller Init");
            var res = await _mediator.Send(updateRightCommand);
            _logger.LogInformation("update Right in Controller finished");
            return Ok(res);
        }

        [HttpGet]
        [Route("GetRightById")]
        public async Task<ActionResult> GetRightById(Guid Id) {

            _logger.LogInformation("Get Right By Id in Controller Init");
            var res = await _mediator.Send(new GetRightByIdQuery() { Id = Id});
            _logger.LogInformation("Get Right By Id in Controller finished");
            return Ok(res);

        }

        [HttpGet]
        [Route("IsRightExist")]
        public async Task<ActionResult> IsRightExist(Guid id, string right) {
            _logger.LogInformation("IsRight Exist in Controller Init");
            var res = await _mediator.Send(new GetRightByNameQuery() { Id = id, Right = right });
            _logger.LogInformation("Is Right Exist in Controller finished");
            return Ok(res);
        }
    }
}
