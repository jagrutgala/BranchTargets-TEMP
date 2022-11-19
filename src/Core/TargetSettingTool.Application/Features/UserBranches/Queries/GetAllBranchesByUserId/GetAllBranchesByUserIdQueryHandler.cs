using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Exceptions;
using TargetSettingTool.Application.Features.UserBranches.Queries.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.UserBranches.Queries.GetAllBranchesByUserId
{
    public class GetAllBranchesByUserIdQueryHandler : IRequestHandler<GetAllBranchesByUserIdQuery, Response<IEnumerable<UserBranchDto>>>
    {
        private readonly ILogger<GetAllBranchesByUserIdQueryHandler> _logger;
        private readonly IUserBranchRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBranchesByUserIdQueryHandler(
            ILogger<GetAllBranchesByUserIdQueryHandler> logger,
            IUserBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<UserBranchDto>>> Handle(GetAllBranchesByUserIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            IEnumerable<UserBranch> userBranches = await _repository.GetBranchesByUserIdAsync(request.UserId);
            userBranches = userBranches
                .Where(x => x.IsDeleted == false);
            var response = new Response<IEnumerable<UserBranchDto>>(_mapper.Map<IEnumerable<UserBranchDto>>(userBranches));
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
