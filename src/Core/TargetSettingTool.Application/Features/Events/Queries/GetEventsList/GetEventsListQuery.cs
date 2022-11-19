using TargetSettingTool.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace TargetSettingTool.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQuery: IRequest<Response<IEnumerable<EventListVm>>>
    {

    }
}
