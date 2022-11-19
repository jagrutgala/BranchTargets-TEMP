using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Features.UserParameters.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.UserParameters.Queries
{
    public class GetAllUserParametersByBranchIdQueryHandler : IRequestHandler<GetAllUserParametersByBranchIdQuery, Response<IEnumerable<UserParameterDto>>>
    {
        private readonly ILogger<GetAllUserParametersByBranchIdQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IUserParameterRepository _userParameterRepository;

        public GetAllUserParametersByBranchIdQueryHandler(
            ILogger<GetAllUserParametersByBranchIdQueryHandler> logger,
            IMapper mapper,
            IUserParameterRepository userParameterRepository
        )
        {
            _logger = logger;
            _mapper = mapper;
            _userParameterRepository = userParameterRepository;
        }

        public async Task<Response<IEnumerable<UserParameterDto>>> Handle(GetAllUserParametersByBranchIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            IEnumerable<UserParameter> userParameters = await _userParameterRepository
                .GetAllUserParametersByBranchId(request.BranchId);
            userParameters = userParameters.Where(x => x.IsDeleted == false);
            var response = new Response<IEnumerable<UserParameterDto>>(_mapper.Map<IEnumerable<UserParameterDto>>(userParameters));
            _logger.LogInformation("Handler Completed");
            return response;

        }
    }
}
