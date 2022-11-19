using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Exceptions;
using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Branches.Queries.GetBranchById
{
    public class GetBranchByIdQueryHandler : IRequestHandler<GetBranchByIdQuery, Response<BranchDto>>
    {
        private readonly ILogger<GetBranchByIdQueryHandler> _logger;
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public GetBranchByIdQueryHandler(
            ILogger<GetBranchByIdQueryHandler> logger,
            IBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<BranchDto>> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            Branch branch = await _repository.GetByIdAsync(request.Id);
            if (branch is null || branch.IsDeleted == true)
            {
                throw new NotFoundException($"Branch", request.Id);
            }
            var response = new Response<BranchDto>(_mapper.Map<Branch, BranchDto>(branch));
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
