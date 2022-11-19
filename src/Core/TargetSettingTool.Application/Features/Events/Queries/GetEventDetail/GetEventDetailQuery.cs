using TargetSettingTool.Application.Responses;
using MediatR;
using System;

namespace TargetSettingTool.Application.Features.Events.Queries.GetEventDetail
{
    public class GetEventDetailQuery: IRequest<Response<EventDetailVm>>
    {
        public string Id { get; set; }
    }
}
