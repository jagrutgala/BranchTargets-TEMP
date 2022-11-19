using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Features.UserBranches.Queries.GetAllBranchesByUserId;
using TargetSettingTool.Application.Features.UserBranches.Queries.GetBranchDropdown;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class UserBranchController : ControllerBase
    {
        private readonly ILogger<UserBranchController> _logger;
        private readonly IMediator _mediator;
        public UserBranchController(ILogger<UserBranchController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> GetBranchDropdown()
        {
            _logger.LogInformation("GetBranchDropdown Query Initiated");
            var response = await _mediator.Send(new GetBranchDropdownQuery());
            _logger.LogInformation("GetBranchDropdown Query Initiated");
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetBranchesByUserId(Guid userId)
        {
            _logger.LogInformation("GetBranchDropdown Query Initiated");
            var response = await _mediator.Send(new GetAllBranchesByUserIdQuery(){UserId = userId});
            _logger.LogInformation("GetBranchDropdown Query Initiated");
            return Ok(response);
        }
    }
}
