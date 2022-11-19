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
using TargetSettingTool.Application.Features.Parameters.Queries.GetParametersById;
using TargetSettingTool.Application.Responses;

namespace TargetSettingTool.Application.Features.Parameters.Queries.GetParameterById
{
    public class GetParameterByIdQueryHandler : IRequestHandler<GetParameterByIdQuery, Response<GetParameterByIdVm>>
    {
        private readonly IParameterRepository _parameterRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetParameterByIdQueryHandler> _logger;
        public GetParameterByIdQueryHandler(IParameterRepository parameterRepository, IMapper mapper, ILogger<GetParameterByIdQueryHandler> logger)
        {
            _parameterRepository = parameterRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<GetParameterByIdVm>> Handle(GetParameterByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetParameterById Handler Initiated");
            var getParameter = await _parameterRepository.GetByIdAsync(request.Id);
            if (getParameter is null || getParameter.IsDeleted == true)
            {
                throw new NotFoundException($"GetRoleById", request.Id);
            }
            var response = new Response<GetParameterByIdVm>(_mapper.Map<GetParameterByIdVm>(getParameter));
            _logger.LogInformation("GetParameterById Handler Completed");
            return response;
        }
    }
}
