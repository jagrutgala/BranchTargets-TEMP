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
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.ParameterTypes.Commands.UpdateParameterType
{
    public class UpdateParameterTypeCommandHandler : IRequestHandler<UpdateParameterTypeCommand, Response<bool>>
    {
        private readonly IParameterTypeRepository _parameterTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateParameterTypeCommandHandler> _logger;
        public UpdateParameterTypeCommandHandler(IParameterTypeRepository parameterTypeRepository, IMapper mapper, ILogger<UpdateParameterTypeCommandHandler> logger)
        {
            _parameterTypeRepository = parameterTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> Handle(UpdateParameterTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("UpdateParameterType Command Handler Initiated");
            var parameterTypeToUpdate = await _parameterTypeRepository.GetByIdAsync(request.Id);
            if (parameterTypeToUpdate == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.ParameterType), request.Id);
            }
            _mapper.Map(request, parameterTypeToUpdate);
            parameterTypeToUpdate.LastModifiedDate = DateTime.Now;
            parameterTypeToUpdate.LastModifiedBy = request.LoggedInUserId;
            await _parameterTypeRepository.UpdateAsync(parameterTypeToUpdate);
            _logger.LogInformation("UpdateParameterType Command Handler Completed");
            return new Response<bool>(true, "ParameterType Updated Successfully");
        }
    }
}
