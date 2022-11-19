using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Exceptions;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.UserBranches.Commands.UpdateUserBranch
{
    public class UpdateUserBranchCommandHandler : IRequestHandler<UpdateUserBranchCommand, Response<bool>>
    {
        private readonly ILogger<UpdateUserBranchCommandHandler> _logger;
        private readonly IUserBranchRepository _repository;
        private readonly IMapper _mapper;

        public UpdateUserBranchCommandHandler(
            ILogger<UpdateUserBranchCommandHandler> logger,
            IUserBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(UpdateUserBranchCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            UserBranch userBranchToUpdate = await _repository.GetByIdAsync(request.Id);
            if (userBranchToUpdate is null || userBranchToUpdate.IsDeleted == true)
            {
                throw new NotFoundException($"Branch", request.Id);
            }
            _mapper.Map<UpdateUserBranchCommand, UserBranch>(request, userBranchToUpdate);
            userBranchToUpdate.LastModifiedBy = request.LoggedInUserId;
            userBranchToUpdate.LastModifiedDate = DateTime.Now;
            await _repository.UpdateAsync(userBranchToUpdate);
            var response = new Response<bool>(true, "Updated UserBranch");
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
