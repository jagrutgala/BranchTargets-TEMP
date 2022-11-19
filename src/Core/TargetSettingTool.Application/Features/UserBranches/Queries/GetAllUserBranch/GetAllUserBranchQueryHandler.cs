using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Features.UserBranches.Queries.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.UserBranches.Queries.GetAllUserBranch
{
    public class GetAllUserBranchQueryHandler : IRequestHandler<GetAllUserBranchQuery, Response<IEnumerable<UserBranchDto>>>
    {
        private readonly ILogger<GetAllUserBranchQueryHandler> _logger;
        private readonly IUserBranchRepository _repository;
        private readonly IMapper _mapper;

        public GetAllUserBranchQueryHandler(
            ILogger<GetAllUserBranchQueryHandler> logger,
            IUserBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<UserBranchDto>>> Handle(GetAllUserBranchQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            IEnumerable<UserBranch> userBranch = await _repository.ListAllAsync();
            userBranch = userBranch.Where(x => x.IsDeleted == false).OrderByDescending(u => u.CreatedDate);
            var response = new Response<IEnumerable<UserBranchDto>>(_mapper.Map<IEnumerable<UserBranchDto>>(userBranch));
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
