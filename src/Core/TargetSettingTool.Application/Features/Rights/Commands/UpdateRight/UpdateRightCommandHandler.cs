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

namespace TargetSettingTool.Application.Features.Rights.Commands.UpdateRight
{
    public class UpdateRightCommandHandler : IRequestHandler<UpdateRightCommand, Response<bool>>
    {
        private readonly ILogger<UpdateRightCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRightRepository _rightRepository;

        public UpdateRightCommandHandler(ILogger<UpdateRightCommandHandler> logger, IMapper mapper, IRightRepository rightRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rightRepository = rightRepository;
        }
        public  async Task<Response<bool>> Handle(UpdateRightCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update Command Handler Init");
            var righttoUpdate = await _rightRepository.GetByIdAsync(request.Id);
            if (righttoUpdate == null || righttoUpdate.IsDeleted == true)
            {
                throw new NotFoundException(nameof(Right), request.Id);
            }
            _mapper.Map<UpdateRightCommand,Right>(request,righttoUpdate);
            righttoUpdate.LastModifiedBy = request.LoggedIn;
            righttoUpdate.LastModifiedDate = DateTime.Now;
            await _rightRepository.UpdateAsync(righttoUpdate);
            return new Response<bool>(true);
        }
    }
}
