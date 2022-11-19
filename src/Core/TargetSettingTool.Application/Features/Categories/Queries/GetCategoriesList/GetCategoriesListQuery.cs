using TargetSettingTool.Application.Responses;
using MediatR;
using System.Collections.Generic;

namespace TargetSettingTool.Application.Features.Categories.Queries.GetCategoriesList
{
    public class GetCategoriesListQuery : IRequest<Response<IEnumerable<CategoryListVm>>>
    {
    }
}
