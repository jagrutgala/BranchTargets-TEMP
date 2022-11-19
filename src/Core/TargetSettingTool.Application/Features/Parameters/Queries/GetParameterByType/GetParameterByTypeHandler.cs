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

namespace TargetSettingTool.Application.Features.Parameters.Queries.GetParameterByType
{
    public class GetParameterByTypeHandler : IRequestHandler<GetParameterByTypeQuery, Response<bool>>
    {
        private readonly IParameterRepository _parameterRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetParameterByTypeHandler> _logger;
        public GetParameterByTypeHandler(IParameterRepository parameterRepository, IMapper mapper, ILogger<GetParameterByTypeHandler> logger)
        {
            _parameterRepository = parameterRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<bool>> Handle(GetParameterByTypeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetParameterByType Handler Initiated");
            bool getRole = await _parameterRepository.GetParameterByTypeAsync(request.Name);
            if(!getRole)
            {
                return new Response<bool>(false);
            }
            _logger.LogInformation("GetRoleByName Handler Completed");
            return new Response<bool>(true);
        }
    }
}
