using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.SideBar.Queries.GetSideBar
{
    public class GetSideBarHandlerQuery : IRequestHandler<GetSideBarQuery, Response<List<GetSideBarVm>>>
    {
        ILogger<GetSideBarHandlerQuery> _logger;
        ISideBarMenuRepository _sideBarMenuRepository;
        IMapper _mapper ;

        public GetSideBarHandlerQuery(ILogger<GetSideBarHandlerQuery> logger,ISideBarMenuRepository sideBarMenuRepository,IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _sideBarMenuRepository = sideBarMenuRepository;
            
        }
        public async Task<Response<List<GetSideBarVm>>> Handle(GetSideBarQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("SideBar Menu Init");
            var sideBarMenu = await _sideBarMenuRepository.GetSideBarList();
            var result = _mapper.Map<List<GetSideBarVm>>(sideBarMenu);
            var response = new Response<List<GetSideBarVm>>(result);
            _logger.LogInformation("SideBar Menu Finished");
            return response;
        }
    }
}
