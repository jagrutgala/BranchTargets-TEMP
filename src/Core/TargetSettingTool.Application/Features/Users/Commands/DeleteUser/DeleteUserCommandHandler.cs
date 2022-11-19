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
using TargetSettingTool.Application.Features.Users.Commands.CreateUser;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Response<bool>>
    {
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public DeleteUserCommandHandler(ILogger<DeleteUserCommandHandler> logger, IUserRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            User updatedUser = await _repository.GetByIdAsync(request.Id);
            if (updatedUser is null || updatedUser.IsDeleted == true)
            {
                throw new NotFoundException($"User", request.Id);
            }
            updatedUser.IsDeleted = true;
            updatedUser.DeletedDate = DateTime.Now;
            updatedUser.DeletedBy = request.LoggedInUserId;
            await _repository.UpdateAsync(updatedUser);
            var response = new Response<bool>(true, "Deleted User");
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
