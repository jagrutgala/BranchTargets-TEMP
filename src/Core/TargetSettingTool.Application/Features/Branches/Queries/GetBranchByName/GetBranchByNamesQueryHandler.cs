using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Exceptions;
using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Branches.Queries.GetBranchByName
{
    public class GetBranchByNameQueryHandler : IRequestHandler<GetBranchByNameQuery, Response<BranchDto>>
    {
        private readonly ILogger<GetBranchByNameQueryHandler> _logger;
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public GetBranchByNameQueryHandler(
            ILogger<GetBranchByNameQueryHandler> logger,
            IBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<BranchDto>> Handle(GetBranchByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            Branch branch = await _repository.GetByNameAsync(request.Name);
            if (branch is null || branch.IsDeleted == true)
            {
                throw new NotFoundException($"Branch", request.Name);
            }
            var response = new Response<BranchDto>(_mapper.Map<Branch, BranchDto>(branch));
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
