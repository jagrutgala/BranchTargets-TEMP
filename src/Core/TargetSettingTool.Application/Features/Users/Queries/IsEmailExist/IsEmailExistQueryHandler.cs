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
using TargetSettingTool.Application.Responses;
using TargetSettingTool.Domain.Entities;

namespace TargetSettingTool.Application.Features.Users.Queries.IsEmailExist
{
    public class IsEmailExistQueryHandler : IRequestHandler<IsEmailExistQuery, Response<bool>>
    {
        private readonly ILogger<IsEmailExistQueryHandler> _logger;
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public IsEmailExistQueryHandler(ILogger<IsEmailExistQueryHandler> logger, IUserRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(IsEmailExistQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handler Initalized");
            User user = await _repository.GetUserByEmailAsync(request.Email);
            var response = new Response<bool>(user== null || user.Id == request.Id);
            _logger.LogInformation("Handler Completed");
            return response;
        }
    }
}
