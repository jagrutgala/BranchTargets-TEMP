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

namespace TargetSettingTool.Application.Features.Rights.Queries.GetAllRights
{
    public class GetAllRightsQueryHandler : IRequestHandler<GetAllRightsQuery, Response<IEnumerable<GetAllRightsVm>>>
    {
        private readonly ILogger<GetAllRightsQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRightRepository _rightRepository;

        public GetAllRightsQueryHandler(ILogger<GetAllRightsQueryHandler> logger,IMapper mapper,IRightRepository rightRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _rightRepository = rightRepository;
        }

        public async Task<Response<IEnumerable<GetAllRightsVm>>> Handle(GetAllRightsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("GetAllRights Initilized");
            List<Right> allRights = (List<Right>)await _rightRepository.ListAllAsync();
            allRights = allRights.Where(x => x.IsDeleted == false).ToList();
            var allRightsDto = _mapper.Map<IEnumerable<GetAllRightsVm>>(allRights);
            var res = new Response<IEnumerable<GetAllRightsVm>>(allRightsDto);
            _logger.LogInformation("GetAllRights Finished");
            return res;
        }
    }
}
