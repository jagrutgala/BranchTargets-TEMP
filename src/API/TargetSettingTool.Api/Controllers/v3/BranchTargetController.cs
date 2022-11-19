using AutoMapper;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Features.BranchTargets.Commands;
using TargetSettingTool.Application.Features.BranchTargets.Queries;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiController]
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BranchTargetController : ControllerBase
    {
        private readonly ILogger<BranchTargetController> _logger;
        private readonly IMediator _mediator;

        public BranchTargetController(
            ILogger<BranchTargetController> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetAllTargets")]
        public async Task<IActionResult> GetAllTargets(Guid branchId, DateTime startDate)
        {
            _logger.LogInformation("GetBranchByName Query Initiated");
            var response = await _mediator.Send(new GetAllBranchTargetsQuery()
            {
                BranchId = branchId,
                StartDate = startDate
            });
            _logger.LogInformation("GetBranchByName Query Initiated");
            return Ok(response);
        }

        [HttpPost]
        [Route("UpdateTargets")]
        public async Task<IActionResult> UpdateTarget(UpdateBranchTargetsCommand ubtsc)
        {
            _logger.LogInformation("GetBranchByName Query Initiated");
            var response = await _mediator.Send(ubtsc);
            _logger.LogInformation("GetBranchByName Query Initiated");
            return Ok(response);
        }

    }
}
