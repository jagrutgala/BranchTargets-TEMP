using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using MediatR;

using Microsoft.Extensions.Logging;

using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Application.Features.Users.Queries.Common;
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Response<IEnumerable<UserDto>>>
    {
        private readonly ILogger<GetAllUsersQueryHandler> _logger;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(ILogger<GetAllUsersQueryHandler> logger, IUserRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<UserDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            List<User> user = (List<User>)await _repository.GetAllAsync();
            user = user.ToList();
            var response = new Response<IEnumerable<UserDto>>(_mapper.Map<IEnumerable<UserDto>>(user));
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
