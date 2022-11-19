using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Exceptions;
using TargetSettingTool.Application.Features.Users.Queries.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Users.Queries.GetUsersWhereRoleIsRegion
{
    public class GetUsersWhereRoleIsRegionQueryHandler : IRequestHandler<GetUsersWhereRoleIsRegionQuery, Response<IEnumerable<UserDto>>>
    {
        private readonly ILogger<GetUsersWhereRoleIsRegionQueryHandler> _logger;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUsersWhereRoleIsRegionQueryHandler(
            ILogger<GetUsersWhereRoleIsRegionQueryHandler> logger,
            IUserRepository repository,
            IMapper mapper
        )
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<UserDto>>> Handle(GetUsersWhereRoleIsRegionQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            IEnumerable<User> users = await _repository.GetUsersByRoleName(request.Role);
            if (users is null )
            {
                throw new NotFoundException("User", "Region");
            }
            users = users.Where(x => x.IsDeleted == false);
            var response = new Response<IEnumerable<UserDto>>(_mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users));
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
