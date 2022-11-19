using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Features.Branches.Queries.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.UserBranches.Queries.GetBranchDropdown
{
    public class GetBranchDropdownQueryHandler : IRequestHandler<GetBranchDropdownQuery, Response<IEnumerable<BranchDto>>>
    {
        private readonly ILogger<GetBranchDropdownQueryHandler> _logger;
        private readonly IBranchRepository _branchRepository;
        private readonly IUserBranchRepository _userBranchRepository;
        private readonly IMapper _mapper;

        public GetBranchDropdownQueryHandler(
            ILogger<GetBranchDropdownQueryHandler> logger,
            IBranchRepository branchRepository,
            IUserBranchRepository userBranchRepository,
            IMapper mapper
        )
        {
            _logger = logger;
            _branchRepository = branchRepository;
            _userBranchRepository = userBranchRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<BranchDto>>> Handle(GetBranchDropdownQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            IEnumerable<Branch> allBranches = await _branchRepository.ListAllAsync();
            IEnumerable<Branch> userbranches = await _userBranchRepository.GetUniqueBranches();
            IEnumerable<BranchDto> branchList = _mapper.Map<IEnumerable<BranchDto>>(allBranches.Except(userbranches)) ?? new List<BranchDto>();
            Response<IEnumerable<BranchDto>> response = new Response<IEnumerable<BranchDto>>(branchList);
            _logger.LogInformation("Handler Completed");
            return response;
            //allBranches = _branchRepositroy.GetAll();
            //branches = _userBranchRepository.GetUniqueBranches()
            //allBranches.Except(branches);
        }
    }
}
