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

namespace TargetSettingTool.Application.Features.ParameterTypes.Queries.GetParameterTypeByName
{
    public class GetParameterTypeByNameQueryHandler : IRequestHandler<GetParameterTypeByNameQuery, Response<bool>>
    {
        private readonly IParameterTypeRepository _parameterTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetParameterTypeByNameQueryHandler> _logger;
        public GetParameterTypeByNameQueryHandler(IParameterTypeRepository parameterTypeRepository, IMapper mapper, ILogger<GetParameterTypeByNameQueryHandler> logger)
        {
            _parameterTypeRepository = parameterTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> Handle(GetParameterTypeByNameQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetParameterTypeByName Handler Initiated");
            var parameterType = await _parameterTypeRepository.GetParameterTypeByNameAsync(request.Name);
            var response = new Response<bool>(parameterType == null || parameterType.Id == request.Id);
            _logger.LogInformation("GetParameterTypeByName Handler Completed");
            return response;
        }
    }
}
