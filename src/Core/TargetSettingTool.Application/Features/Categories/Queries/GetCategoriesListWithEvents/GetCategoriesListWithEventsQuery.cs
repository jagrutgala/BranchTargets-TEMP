using TargetSettingTool.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace TargetSettingTool.Application.Features.Categories.Queries.GetCategoriesListWithEvents
{
    public class GetCategoriesListWithEventsQuery: IRequest<Response<IEnumerable<CategoryEventListVm>>>
    {
        public bool IncludeHistory { get; set; }
    }
}
