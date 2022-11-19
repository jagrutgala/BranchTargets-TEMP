using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Features.BranchTargets.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.BranchTargets.Queries
{
    public class GetAllBranchTargetsQueryHandler : IRequestHandler<GetAllBranchTargetsQuery, Response<IEnumerable<BranchTargetDto>>>
    {
        private readonly ILogger<GetAllBranchTargetsQueryHandler> _logger;
        private readonly IBranchTargetRepository _branchTargetRepository;
        private readonly IMapper _mapper;

        public GetAllBranchTargetsQueryHandler(
            ILogger<GetAllBranchTargetsQueryHandler> logger,
            IBranchTargetRepository branchTargetRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _branchTargetRepository = branchTargetRepository;
            _mapper = mapper;
        }
        public async Task<Response<IEnumerable<BranchTargetDto>>> Handle(GetAllBranchTargetsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            IEnumerable<BranchTarget> branchTargets = await _branchTargetRepository
                .GetBranchTargetByBranchIdAndStartDate(request.BranchId, request.StartDate);
            branchTargets = branchTargets.Where(x => x.IsDeleted == false);
            var response = new Response<IEnumerable<BranchTargetDto>>(_mapper.Map<IEnumerable<BranchTargetDto>>(branchTargets));
            _logger.LogInformation("Handler Completed");
            return response;

        }
    }
}
