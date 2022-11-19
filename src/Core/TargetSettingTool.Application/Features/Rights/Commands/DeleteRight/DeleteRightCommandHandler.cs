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

namespace TargetSettingTool.Application.Features.Rights.Commands.DeleteRight
{
    public class DeleteRightCommandHandler : IRequestHandler<DeleteRightCommand, Response<bool>>
    {
        private readonly ILogger<DeleteRightCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRightRepository _rightRepository;

        public DeleteRightCommandHandler(ILogger<DeleteRightCommandHandler> logger, IMapper mapper, IRightRepository rightRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rightRepository = rightRepository;
        }

        public async Task<Response<bool>> Handle(DeleteRightCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Delete Init");
            Right right = await _rightRepository.GetByIdAsync(request.Id);
            if (right != null)
            {
                right.DeletedDate = DateTime.Now;
                right.IsDeleted = true;
                right.DeletedBy = request.LoggedIn;
                await _rightRepository.UpdateAsync(right);
                return new Response<bool>(true,"Right Deleted Successfully");
            }
            else {
                return new Response<bool>(false, "Right Not Found");
            }
        }
    }
}
