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

namespace TargetSettingTool.Application.Features.Branches.Commands.CreateBranch
{
    public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, Response<Guid>>
    {
        private readonly ILogger<CreateBranchCommandHandler> _logger;
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public CreateBranchCommandHandler(
            ILogger<CreateBranchCommandHandler> logger,
            IBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            Branch branchToAdd = _mapper.Map<Branch>(request);
            branchToAdd.CreatedDate = DateTime.Now;
            branchToAdd.CreatedBy = request.LoggedInUserId;
            Branch branch = await _repository.AddAsync(branchToAdd);
            var response = new Response<Guid>(branch.Id, "Created Branch");
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
