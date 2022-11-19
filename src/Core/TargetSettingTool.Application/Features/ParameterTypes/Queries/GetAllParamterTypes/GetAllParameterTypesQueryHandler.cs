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

namespace TargetSettingTool.Application.Features.ParameterTypes.Queries.GetAllParamterTypes
{
    public class GetAllParameterTypesQueryHandler : IRequestHandler<GetAllParameterTypesQuery, Response<IEnumerable<ParameterTypesDto>>>
    {
        private readonly IParameterTypeRepository _parameterTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllParameterTypesQueryHandler> _logger;

        public GetAllParameterTypesQueryHandler(IParameterTypeRepository parameterTypeRepository, IMapper mapper, ILogger<GetAllParameterTypesQueryHandler> logger)
        {
            _parameterTypeRepository = parameterTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<ParameterTypesDto>>> Handle(GetAllParameterTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllParameterTypes Query Handler Initiated");
            List<Domain.Entities.ParameterType> getAllParameterTypes = (List<Domain.Entities.ParameterType>)await _parameterTypeRepository.ListAllAsync();
            getAllParameterTypes = getAllParameterTypes.Where(x => x.IsDeleted == false).ToList();
            var parameterTypeList = _mapper.Map<IEnumerable<ParameterTypesDto>>(getAllParameterTypes);
            var response = new Response<IEnumerable<ParameterTypesDto>>(parameterTypeList);
            _logger.LogInformation("GetAllParameterTypes Query Handler Completed");
            return response;
        }
    }
}
