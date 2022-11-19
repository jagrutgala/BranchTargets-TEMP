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
using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;

namespace TargetSettingTool.Application.Features.Parameters.Commands.DeleteParameter
{
    public class DeleteParameterCommandHandler : IRequestHandler<DeleteParameterCommand, Response<bool>>
    {
        private readonly ILogger<DeleteParameterCommandHandler> _logger;
        private readonly IParameterRepository _parameterRepository;
        private readonly IMapper _mapper;
        public DeleteParameterCommandHandler(ILogger<DeleteParameterCommandHandler> logger, IParameterRepository parameterRepository, IMapper mapper)
        {
            _logger = logger;
            _parameterRepository = parameterRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(DeleteParameterCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete Role Handler Initiated");
            var parameterToDelete = await _parameterRepository.GetByIdAsync(request.Id);
            if(parameterToDelete == null || parameterToDelete.IsDeleted == true)
            {
                throw new NotFoundException(nameof(Parameter), request.Id);
            }
            parameterToDelete.IsDeleted = true;
            parameterToDelete.DeletedDate = DateTime.Now;
            parameterToDelete.DeletedBy = request.LoggedInUserId;
            await _parameterRepository.UpdateAsync(parameterToDelete);
            _logger.LogInformation("Delete Parameter Handler Completed");
            return new Response<bool>(true, "Parameter Deleted Successfully");
        }
    }
}
