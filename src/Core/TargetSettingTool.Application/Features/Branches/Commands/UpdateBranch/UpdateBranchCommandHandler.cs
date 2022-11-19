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

namespace TargetSettingTool.Application.Features.Branches.Commands.UpdateBranch
{
    public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, Response<bool>>
    {
        private readonly ILogger<UpdateBranchCommandHandler> _logger;
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public UpdateBranchCommandHandler(
            ILogger<UpdateBranchCommandHandler> logger,
            IBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            Branch branchToUpdate = await _repository.GetByIdAsync(request.Id);
            if (branchToUpdate is null || branchToUpdate.IsDeleted == true)
            {
                throw new NotFoundException($"Branch", request.Id);
            }
            _mapper.Map<UpdateBranchCommand, Branch>(request, branchToUpdate);
            branchToUpdate.LastModifiedBy = request.LoggedInUserId;
            branchToUpdate.LastModifiedDate = DateTime.Now;
            await _repository.UpdateAsync(branchToUpdate);
            var response = new Response<bool>(true, "Updated Branch");
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
