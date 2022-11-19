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

namespace TargetSettingTool.Application.Features.Rights.Queries.GetRightById
{
   
    public class GetRightByIdHandler : IRequestHandler<GetRightByIdQuery, Response<GetRightByIdVm>>
    {
        private readonly ILogger<GetRightByIdHandler> _logger;
        private readonly IRightRepository _rightRepository;
        private readonly IMapper _mapper;

        public GetRightByIdHandler(ILogger<GetRightByIdHandler> logger,IRightRepository rightRepository,IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _rightRepository = rightRepository;
        }

        public async Task<Response<GetRightByIdVm>> Handle(GetRightByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetRightByIdHandler Initalized");
            Right right = await _rightRepository.GetByIdAsync(request.Id);
            if (right is null || right.IsDeleted == true)
            {
                throw new NotFoundException($"Rights", request.Id);
            }
            var response = _mapper.Map<GetRightByIdVm>(right);
            var res = new Response<GetRightByIdVm>(response);
            _logger.LogInformation("Handler Completed");
            return res;
        }
    }
}
