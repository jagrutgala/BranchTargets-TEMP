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

namespace TargetSettingTool.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Response<bool>>
    {
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IUserBranchRepository _userBranchRepository;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(ILogger<UpdateUserCommandHandler> logger, IUserRepository userRepository, IUserBranchRepository userBranchRepository, IMapper mapper)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userBranchRepository = userBranchRepository;
            _mapper = mapper;
        }
        public async Task<Response<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            User userToUpdate = await _userRepository.GetByIdAsync(request.Id);
            if (userToUpdate is null || userToUpdate.IsDeleted == true)
            {
                throw new NotFoundException($"User", request.Id);
            }
            _mapper.Map<UpdateUserCommand, User>(request, userToUpdate);
            userToUpdate.LastModifiedBy = request.LoggedInUserId;
            userToUpdate.LastModifiedDate = DateTime.Now;
            await _userRepository.UpdateAsync(userToUpdate);
            //UserBranches Delete
            List<UserBranch> branchList = await _userBranchRepository.GetBranchesByUserIdAsync(userToUpdate.Id);
            bool success = await _userBranchRepository.RemoveUserBranchRangeAsync(branchList);
            // UserBranches Add
            List<UserBranch> userBranchesToAdd = new List<UserBranch>();
            if (request.Branches != null && request.Branches.Count() > 0)
            {
                foreach (Guid branchId in request.Branches)
                {
                    userBranchesToAdd.Add(new UserBranch { UserId = userToUpdate.Id, BranchId = branchId, CreatedBy = request.LoggedInUserId, CreatedDate = DateTime.Now });
                }
                List<UserBranch> userBranches = await _userBranchRepository.AddUserBranchRangeAsync(userBranchesToAdd);
            }
            var response = new Response<bool>(true, "Updated User");
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
