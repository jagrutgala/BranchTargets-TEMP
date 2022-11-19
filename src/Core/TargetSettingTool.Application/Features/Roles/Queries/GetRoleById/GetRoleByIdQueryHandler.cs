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

namespace TargetSettingTool.Application.Features.Roles.Queries.GetRoleById
{
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdVm>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetRoleByIdQueryHandler> _logger;
        public GetRoleByIdQueryHandler(IRoleRepository roleRepository, IMapper mapper, ILogger<GetRoleByIdQueryHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<GetRoleByIdVm>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetRoleById Handler Initiated");
            var getRole = await _roleRepository.GetByIdAsync(request.Id);
            if (getRole is null || getRole.IsDeleted == true)
            {
                throw new NotFoundException($"GetRoleById", request.Id);
            }
            var response = new Response<GetRoleByIdVm>(_mapper.Map<GetRoleByIdVm>(getRole));
            _logger.LogInformation("GetRoleById Handler Completed");
            return response;
        }
    }
}
