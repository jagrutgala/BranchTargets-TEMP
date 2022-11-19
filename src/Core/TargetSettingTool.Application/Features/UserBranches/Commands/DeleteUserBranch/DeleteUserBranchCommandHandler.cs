using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Exceptions;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.UserBranches.Commands.DeleteUserBranch
{
    public class DeleteUserBranchCommandHandler : IRequestHandler<DeleteUserBranchCommand, Response<bool>>
    {
        private readonly ILogger<DeleteUserBranchCommandHandler> _logger;
        private readonly IUserBranchRepository _repository;
        private readonly IMapper _mapper;

        public DeleteUserBranchCommandHandler(
            ILogger<DeleteUserBranchCommandHandler> logger,
            IUserBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(DeleteUserBranchCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            UserBranch userBranchToUpdate = await _repository.GetByIdAsync(request.Id);
            if (userBranchToUpdate is null || userBranchToUpdate.IsDeleted == true)
            {
                throw new NotFoundException("UserBranch", request.Id);
            }
            userBranchToUpdate.IsDeleted = true;
            userBranchToUpdate.DeletedDate = DateTime.Now;
            userBranchToUpdate.DeletedBy = request.LoggedInUserId;
            await _repository.UpdateAsync(userBranchToUpdate);
            var response = new Response<bool>(true, "Deleted User Branch");
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
