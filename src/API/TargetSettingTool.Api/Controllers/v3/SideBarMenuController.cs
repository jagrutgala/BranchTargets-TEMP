using MediatR;

using Microsoft.AspNetCore.Mvc;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Features.SideBar.Queries.GetSideBar;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Api.Controllers.v3
{

    [ApiVersion("3")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SideBarMenuController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SideBarMenuController> _logger;

        public SideBarMenuController(ILogger<SideBarMenuController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetMenu")]
        public async Task<ActionResult> GetSideBarMenuList()
        {
            _logger.LogInformation("SideBar Menu Controller Init");
            var sideBarList = await _mediator.Send(new GetSideBarQuery());
            _logger.LogInformation("SideBar Menu Controller finised");
            return Ok(sideBarList);
        }

    }
}
