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

namespace TargetSettingTool.Application.Features.Roles.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, Response<Guid>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoleCommandHandler> _logger;

        public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper, ILogger<CreateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create Handler Initiated");
            var role = _mapper.Map<Role>(request);
            role.CreatedBy = request.LoggedInUserId;
            role.CreatedDate = DateTime.Now;
            try
            {
                var createdRole = await _roleRepository.AddAsync(role);
                return new Response<Guid>(createdRole.Id, "Role Created Successfully");
            }
            catch (System.Exception err)
            {
                _logger.LogInformation(err.Message);
                throw;
            }
            _logger.LogInformation("Create Handler Completed");

        }
    }
}
