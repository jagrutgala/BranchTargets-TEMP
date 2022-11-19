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
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Roles.Queries.GetAllRoles
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Response<IEnumerable<GetAllRolesVm>>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllRolesQueryHandler> _logger;
        public GetAllRolesQueryHandler(IRoleRepository roleRepository, IMapper mapper, ILogger<GetAllRolesQueryHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<GetAllRolesVm>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllRoles Handler Initiated");
            List<Role> getAllRoles = (List<Role>)await _roleRepository.ListAllAsync();
            getAllRoles = getAllRoles.Where(x => x.IsDeleted == false).ToList();
            var roleList = _mapper.Map<IEnumerable<GetAllRolesVm>>(getAllRoles);
            var response = new Response<IEnumerable<GetAllRolesVm>>(roleList);
            _logger.LogInformation("GetAllRoles Handler Completed");
            return response;
        }
    }
}
