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

namespace TargetSettingTool.Application.Features.ParameterTypes.Commands.CreateParameterType
{
    public class CreateParameterTypeCommandHandler : IRequestHandler<CreateParameterTypeCommand, Response<Guid>>
    {
        private readonly ILogger<CreateParameterTypeCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IParameterTypeRepository _parameterTypeRepository;

        public CreateParameterTypeCommandHandler(ILogger<CreateParameterTypeCommandHandler> logger, IMapper mapper, IParameterTypeRepository parameterTypeRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _parameterTypeRepository = parameterTypeRepository;
        }

        public async Task<Response<Guid>> Handle(CreateParameterTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create ParameterType Handler Initiated");
            var parameterType = _mapper.Map<Domain.Entities.ParameterType>(request);
            parameterType.CreatedBy = request.LoggedInUser;
            parameterType.CreatedDate = DateTime.Now;
            var result = await _parameterTypeRepository.AddAsync(parameterType);
            _logger.LogInformation("Create ParameterType Handler Completed");
            var response = new Response<Guid>(result.Id, "ParameterType Created Successfully");
            return response;
        }
    }
}
