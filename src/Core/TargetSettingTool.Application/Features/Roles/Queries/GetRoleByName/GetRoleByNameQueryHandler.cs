using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Exceptions;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Roles.Queries.GetRoleByName
{
    public class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery, Response<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleByNameQueryHandler> _logger;
        public GetRoleByNameQueryHandler(IRoleRepository roleRepository, IMapper mapper, ILogger<GetRoleByNameQueryHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetRoleByName Handler Initiated");
            var getRole = await _roleRepository.GetRoleByNameAsync(request.Name);
            var response = new Response<bool>(getRole == null || getRole.Id == request.Id);
            _logger.LogInformation("GetRoleByName Handler Completed");
            return response;
        }
    }
}
