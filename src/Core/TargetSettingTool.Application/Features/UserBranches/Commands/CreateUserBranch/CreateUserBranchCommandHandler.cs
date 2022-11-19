using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.UserBranches.Commands.CreateUserBranch
{
    public class CreateUserBranchCommandHandler : IRequestHandler<CreateUserBranchCommand, Response<Guid>>
    {
        private readonly ILogger<CreateUserBranchCommandHandler> _logger;
        private readonly IUserBranchRepository _repository;
        private readonly IMapper _mapper;

        public CreateUserBranchCommandHandler(
            ILogger<CreateUserBranchCommandHandler> logger,
            IUserBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateUserBranchCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            UserBranch userBranchToAdd = _mapper.Map<UserBranch>(request);
            userBranchToAdd.CreatedDate = DateTime.Now;
            userBranchToAdd.CreatedBy = request.LoggedInUserId;
            UserBranch branch = await _repository.AddAsync(userBranchToAdd);
            var response = new Response<Guid>(branch.Id, "Created Branch");
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
