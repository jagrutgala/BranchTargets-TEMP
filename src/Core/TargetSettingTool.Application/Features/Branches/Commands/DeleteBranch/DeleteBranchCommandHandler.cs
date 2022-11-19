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

namespace TargetSettingTool.Application.Features.Branches.Commands.DeleteBranch
{
    public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, Response<bool>>
    {
        private readonly ILogger<DeleteBranchCommandHandler> _logger;
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public DeleteBranchCommandHandler(
            ILogger<DeleteBranchCommandHandler> logger,
            IBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            Branch updatedBranch = await _repository.GetByIdAsync(request.Id);
            if (updatedBranch is null || updatedBranch.IsDeleted == true)
            {
                throw new NotFoundException($"Branch", request.Id);
            }
            updatedBranch.IsDeleted = true;
            updatedBranch.DeletedDate = DateTime.Now;
            updatedBranch.DeletedBy = request.LoggedInUserId;
            await _repository.UpdateAsync(updatedBranch);
            var response = new Response<bool>(true, "Deleted Branch");
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
