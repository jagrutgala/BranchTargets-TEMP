using MediatR;

using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Features.Branches.Commands.CreateBranch;
using TargetSettingTool.Application.Features.Branches.Commands.DeleteBranch;
using TargetSettingTool.Application.Features.Branches.Commands.UpdateBranch;
using TargetSettingTool.Application.Features.Branches.Queries.GetAllBranch;
using TargetSettingTool.Application.Features.Branches.Queries.GetBranchById;
using TargetSettingTool.Application.Features.Branches.Queries.GetBranchByName;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiController]
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BranchController : ControllerBase
    {

        private readonly ILogger<BranchController> _logger;
        private readonly IMediator _mediator;
        public BranchController(ILogger<BranchController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateBranch")]
        public async Task<ActionResult> CreateBranch(CreateBranchCommand createBranchCommand)
        {
            _logger.LogInformation("CreateBranch Command Initiated");
            var response = await _mediator.Send(createBranchCommand);
            _logger.LogInformation("CreateBranch Command Completed");
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateBranch")]
        public async Task<ActionResult> UpdateBranch(UpdateBranchCommand updateBranchCommand)
        {
            _logger.LogInformation("UpdateBranch Command Initiated");
            var response = await _mediator.Send(updateBranchCommand);
            _logger.LogInformation("UpdateBranch Command Completed");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllBranches")]
        public async Task<ActionResult> GetAllBranchs()
        {
            _logger.LogInformation("GetAlllBranches Query Initiated");
            var response = await _mediator.Send(new GetAllBranchQuery());
            _logger.LogInformation("GetAlllBranchs Query Initiated");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetBranchById")]
        public async Task<ActionResult> GetBranchById(Guid id)
        {
            _logger.LogInformation("GetBranchById Query Initiated");
            var response = await _mediator.Send(new GetBranchByIdQuery(){Id= id});
            _logger.LogInformation("GetBranchById Query Initiated");
            return Ok(response);
        }

        [HttpGet]
        [Route("GetBranchByName")]
        public async Task<ActionResult> GetBranchByName(string name)
        {
            _logger.LogInformation("GetBranchByName Query Initiated");
            var response = await _mediator.Send(new GetBranchByNameQuery(){Name= name});
            _logger.LogInformation("GetBranchByName Query Initiated");
            return Ok(response);
        }

        [HttpPut]
        [Route("DeleteBranch")]
        public async Task<ActionResult> DeleteBranch(DeleteBranchCommand deleteBranchCommand)
        {
            _logger.LogInformation("DeleteBranch Command Initiated");
            var response = await _mediator.Send(deleteBranchCommand);
            _logger.LogInformation("DeleteBranch Command Completed");
            return Ok(response);
        }

    }
}
