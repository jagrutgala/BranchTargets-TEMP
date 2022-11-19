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

namespace TargetSettingTool.Application.Features.ParameterTypes.Commands.DeleteParameterType
{
    public class DeleteParameterTypeCommandHandler : IRequestHandler<DeleteParameterTypeCommand, Response<bool>>
    {
        private readonly IParameterTypeRepository _parameterTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteParameterTypeCommandHandler> _logger;
        public DeleteParameterTypeCommandHandler(IParameterTypeRepository parameterTypeRepository, IMapper mapper, ILogger<DeleteParameterTypeCommandHandler> logger)
        {
            _parameterTypeRepository = parameterTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> Handle(DeleteParameterTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete ParameterTyper Command Handler Initiated");
            var parameterTypeToDelete = await _parameterTypeRepository.GetByIdAsync(request.Id);
            if (parameterTypeToDelete == null || parameterTypeToDelete.IsDeleted == true)
            {
                throw new NotFoundException(nameof(Domain.Entities.ParameterType), request.Id);
            }
            parameterTypeToDelete.IsDeleted = true;
            parameterTypeToDelete.DeletedDate = DateTime.Now;
            parameterTypeToDelete.DeletedBy = request.LoggedInUserId;
            await _parameterTypeRepository.UpdateAsync(parameterTypeToDelete);
            _logger.LogInformation("Delete ParameterType Command Handler Completed");
            return new Response<bool>(true, "ParameterType Deleted Successfully");
        }
    }
}
