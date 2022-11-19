using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Parameters.Commands.CreateParameters
{
    public class CreateParametersCommandHandler : IRequestHandler<CreateParametersCommand, Response<IEnumerable<Guid>>>
    {
        private readonly IParameterRepository _parameterRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateParametersCommandHandler> _logger;

        public CreateParametersCommandHandler(IParameterRepository parameterRepository, IMapper mapper, ILogger<CreateParametersCommandHandler> logger)
        {
            _parameterRepository = parameterRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<IEnumerable<Guid>>> Handle(CreateParametersCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create Parameters Handler Initiated");
            List<Parameter> parameters = _mapper.Map<List<Parameter>>(request.Parameters);
            foreach (Parameter item in parameters)
            {
                item.CreatedDate = DateTime.Now;
                item.CreatedBy = request.LoggedInUserId; //Session UserId
            }
            var createdRole = await _parameterRepository.AddRangeAsync(parameters);
            _logger.LogInformation("Create Parameters Handler Completed");
            return new Response<IEnumerable<Guid>>(parameters.Select(x => x.Id).ToList(), "Parameters Created Successfully");
        }
    }
}
