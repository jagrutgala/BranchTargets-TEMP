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

namespace TargetSettingTool.Application.Features.Parameters.Commands.CreateParameter
{
    public class CreateParameterCommandHandler : IRequestHandler<CreateParameterCommand, Response<Guid>>
    {
        private readonly IParameterRepository _parameterRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateParameterCommandHandler> _logger;

        public CreateParameterCommandHandler(IParameterRepository parameterRepository, IMapper mapper, ILogger<CreateParameterCommandHandler> logger)
        {
            _parameterRepository = parameterRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<Guid>> Handle(CreateParameterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create Parameter Handler Initiated");
            var parameter = _mapper.Map<Parameter>(request);
            parameter.CreatedDate = DateTime.Now;
            parameter.CreatedBy = request.LoggedInUserId; //Session UserId
            var createdRole = await _parameterRepository.AddAsync(parameter);
            _logger.LogInformation("Create Parameter Handler Completed");
            return new Response<Guid>(parameter.Id, "Parameter Created Successfully");
        }
    }
}
