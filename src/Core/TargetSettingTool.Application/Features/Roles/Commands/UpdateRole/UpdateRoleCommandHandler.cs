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
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Roles.Commands.UpdateRole
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Response<bool>>
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoleCommandHandler> _logger;
        public UpdateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper, ILogger<UpdateRoleCommandHandler> logger)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Handler Initiated");
            var roleToUpdate = await _roleRepository.GetByIdAsync(request.Id);
            if(roleToUpdate == null)
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }
            _mapper.Map<UpdateRoleCommand, Role>(request, roleToUpdate);
            roleToUpdate.LastModifiedDate = DateTime.Now;
            roleToUpdate.LastModifiedBy = request.LoggedInUserId;
            await _roleRepository.UpdateAsync(roleToUpdate);
            _logger.LogInformation("Update Handler Completed");
            return new Response<bool>(true, "Role Updated Successfully");
        }
    }
}
