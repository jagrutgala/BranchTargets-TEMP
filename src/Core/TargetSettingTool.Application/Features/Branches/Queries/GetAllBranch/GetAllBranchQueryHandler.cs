using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Branches.Queries.GetAllBranch
{
    public class GetAllBranchQueryHandler : IRequestHandler<GetAllBranchQuery, Response<IEnumerable<BranchDto>>>
    {
        private readonly ILogger<GetAllBranchQueryHandler> _logger;
        private readonly IBranchRepository _repository;
        private readonly IMapper _mapper;

        public GetAllBranchQueryHandler(
            ILogger<GetAllBranchQueryHandler> logger,
            IBranchRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<BranchDto>>> Handle(GetAllBranchQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            IEnumerable<Branch> branch = await _repository.GetAllAsync();
            branch = branch.Where(x => x.IsDeleted == false);
            var response = new Response<IEnumerable<BranchDto>>(_mapper.Map<IEnumerable<Branch>, IEnumerable<BranchDto>>(branch));
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
