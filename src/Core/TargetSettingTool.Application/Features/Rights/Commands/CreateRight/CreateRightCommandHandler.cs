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

namespace TargetSettingTool.Application.Features.Rights.Commands.CreateRight
{
    public class CreateRightCommandHandler : IRequestHandler<CreateRightCommand, Response<Guid>>
    {
        private readonly ILogger<CreateRightCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRightRepository _rightRepository;

        public CreateRightCommandHandler(ILogger<CreateRightCommandHandler> logger, IMapper mapper, IRightRepository rightRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rightRepository = rightRepository;
        }

        public async Task<Response<Guid>> Handle(CreateRightCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create Rights Init");
            Right right = _mapper.Map<Right>(request);
            right.CreatedBy = request.LoggedIn;
            right.CreatedDate = DateTime.Now;
            var Rights = await _rightRepository.AddAsync(right);
            var res = new Response<Guid>(Rights.Id, "Right Added Succseefully");
            return res;
            
            //return new Response<Guid>(new Guid(),"Failed To Add Role");
        }
    }
}
