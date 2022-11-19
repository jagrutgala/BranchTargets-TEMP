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

namespace TargetSettingTool.Application.Features.Parameters.Commands.UpdateParameter
{
    public class UpdateParameterCommandHandler : IRequestHandler<UpdateParameterCommand, Response<bool>>
    {
        private readonly IParameterRepository _parameterRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateParameterCommandHandler> _logger;
        public UpdateParameterCommandHandler(IParameterRepository parameterRepository, IMapper mapper, ILogger<UpdateParameterCommandHandler> logger)
        {
            _parameterRepository = parameterRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> Handle(UpdateParameterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Parameter Handler Initiated");
            var parameterToUpdate = await _parameterRepository.GetByIdAsync(request.Id);
            if(parameterToUpdate == null)
            {
                throw new NotFoundException(nameof(Parameter), request.Id);
            }
            _mapper.Map<UpdateParameterCommand, Parameter>(request, parameterToUpdate);
            parameterToUpdate.LastModifiedDate = DateTime.Now;
            parameterToUpdate.LastModifiedBy = request.LoggedInUserId;
            await _parameterRepository.UpdateAsync(parameterToUpdate);
            _logger.LogInformation("Update Parameter Handler Completed");
            return new Response<bool>(true, "Parameter Updated Successfully");
        }
    }
}
