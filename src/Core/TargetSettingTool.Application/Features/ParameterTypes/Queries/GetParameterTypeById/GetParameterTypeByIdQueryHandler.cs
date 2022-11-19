using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Exceptions;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.ParameterTypes.Queries.GetParameterTypeById
{
    public class GetParameterTypeByIdQueryHandler : IRequestHandler<GetParameterTypeByIdQuery, Response<GetParameterTypeByIdVm>>
    {
        private readonly IParameterTypeRepository _parameterTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetParameterTypeByIdQueryHandler> _logger;
        public GetParameterTypeByIdQueryHandler(IParameterTypeRepository parameterTypeRepository, IMapper mapper, ILogger<GetParameterTypeByIdQueryHandler> logger)
        {
            _parameterTypeRepository = parameterTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<GetParameterTypeByIdVm>> Handle(GetParameterTypeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetParameterTypeById Handler Initiated");
            var getParameterType = await _parameterTypeRepository.GetByIdAsync(request.Id);
            if (getParameterType is null || getParameterType.IsDeleted == true)
            {
                throw new NotFoundException($"GetParameterTypeById", request.Id);
            }
            var response = new Response<GetParameterTypeByIdVm>(_mapper.Map<GetParameterTypeByIdVm>(getParameterType));
            _logger.LogInformation("GetParameterTypeById Handler Completed");
            return response;
        }
    }
}
