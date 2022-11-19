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
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Parameters.Queries.GetAllParameters
{
    public class GetAllParametersQueryHandler : IRequestHandler<GetAllParametersQuery, Response<IEnumerable<ParameterDto>>>
    {
        private readonly IParameterRepository _parameterRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllParametersQueryHandler> _logger;
        public GetAllParametersQueryHandler(IParameterRepository parameterRepository, IMapper mapper, ILogger<GetAllParametersQueryHandler> logger)
        {
            _parameterRepository = parameterRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<ParameterDto>>> Handle(GetAllParametersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllParameters Handler Initiated");
            List<Parameter> getAllParameters = (List<Parameter>)await _parameterRepository.GetAllParametersAsync();
            getAllParameters = getAllParameters.Where(x => x.IsDeleted == false).ToList();
            var parameterList = _mapper.Map<IEnumerable<ParameterDto>>(getAllParameters);
            var response = new Response<IEnumerable<ParameterDto>>(parameterList);
            _logger.LogInformation("GetAllParameters Handler Completed");
            return response;
        }

    }
}
