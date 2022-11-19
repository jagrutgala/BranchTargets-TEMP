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
using TargetSettingTool.Application.Features.Users.Queries.Common;
using TargetSettingTool.Application.Features.Users.Queries.GetAllUsers;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserDto>>
    {
        private readonly ILogger<GetUserByIdQueryHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserBranchRepository _userBranchRepository;
        private readonly IMapper _mapper;
        public GetUserByIdQueryHandler(ILogger<GetUserByIdQueryHandler> logger, IUserRepository userRepository, IUserBranchRepository userBranchRepository,IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userBranchRepository = userBranchRepository;
            _mapper = mapper;
        }
        public async Task<Response<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            User user = await _userRepository.GetUserByIdAsync(request.Id);
            List<UserBranch> userBranchList = await _userBranchRepository.GetBranchesByUserIdAsync(user.Id);
 
            if (user is null || user.IsDeleted == true)
            {
                throw new NotFoundException($"User", request.Id);
            }
            UserDto userdto = _mapper.Map<User, UserDto>(user);
            userdto.Branches = userBranchList.Select(x => x.BranchId).ToList();
            var response = new Response<UserDto>(userdto);
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
