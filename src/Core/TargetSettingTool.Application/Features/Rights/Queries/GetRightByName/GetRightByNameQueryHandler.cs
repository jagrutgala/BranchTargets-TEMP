using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Rights.Queries.GetRightByName
{
    public class GetRightByNameQueryHandler : IRequestHandler<GetRightByNameQuery, Response<bool>>
    {
        private readonly ILogger<GetRightByNameQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRightRepository _rightRepository;

        public GetRightByNameQueryHandler(ILogger<GetRightByNameQueryHandler> logger, IMapper mapper, IRightRepository rightRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rightRepository = rightRepository;
        }
        public async Task<Response<bool>> Handle(GetRightByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetRightByName Handler Initiated");
            var result = await _rightRepository.IsRightExist(request.Right);
            var response = new Response<bool>(result == null || result.Id == request.Id);
            _logger.LogInformation("GetRightByName Handler Completed");
            return response;
            
        }
    }
}
