using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Features.UserParameters.Queries;

namespace TargetSettingTool.Api.Controllers.v3
{
    [ApiController]
    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserParameterController : ControllerBase
    {
        private readonly ILogger<UserParameterController> _logger;
        private readonly IMediator _mediator;

        public UserParameterController(
            ILogger<UserParameterController> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetUserParametersByBranchId")]
        public async Task<IActionResult> GetUserParametersByBranchId(Guid branchId)
        {
            _logger.LogInformation("GetUserParametersByBranchId Query Initiated");
            var response = await _mediator.Send(new GetAllUserParametersByBranchIdQuery()
            {
                BranchId = branchId
            });
            _logger.LogInformation("GetUserParametersByBranchId Query Initiated");
            return Ok(response);
        }

    }
}
