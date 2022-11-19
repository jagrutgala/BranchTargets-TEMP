using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Exceptions;
using TargetSettingTool.Application.Features.UserBranches.Queries.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.UserBranches.Queries.GetUserBranchById
{
    public class GetBranchByIdQueryHandler : IRequestHandler<GetUserBranchByIdQuery, Response<UserBranchDto>>
    {
        private readonly ILogger<GetBranchByIdQueryHandler> _logger;
        private readonly IUserBranchRepository _repository;
        private readonly IMapper _mapper;

        public GetBranchByIdQueryHandler(
            ILogger<GetBranchByIdQueryHandler> logger,
            IUserBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<UserBranchDto>> Handle(GetUserBranchByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            UserBranch branch = await _repository.GetByIdAsync(request.Id);
            if (branch is null || branch.IsDeleted == true)
            {
                throw new NotFoundException($"Branch", request.Id);
            }
            var response = new Response<UserBranchDto>(_mapper.Map<UserBranchDto>(branch));
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
