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
using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;

namespace TargetSettingTool.Application.Features.Roles.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Response<bool>>
    {
        private readonly ILogger<DeleteRoleCommandHandler> _logger;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public DeleteRoleCommandHandler(ILogger<DeleteRoleCommandHandler> logger, IRoleRepository roleRepository, IMapper mapper)
        {
            _logger = logger;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete Role Handler Initiated");
            var roleToDelete = await _roleRepository.GetByIdAsync(request.Id);
            if(roleToDelete == null || roleToDelete.IsDeleted == true)
            {
                throw new NotFoundException(nameof(Role), request.Id);
            }
            roleToDelete.IsDeleted = true;
            roleToDelete.DeletedDate = DateTime.Now;
            roleToDelete.DeletedBy = request.LoggedInUserId;
            await _roleRepository.UpdateAsync(roleToDelete);
            _logger.LogInformation("Delete Role Handler Completed");
            return new Response<bool>(true, "Role Deleted Successfully");
        }
    }
}
