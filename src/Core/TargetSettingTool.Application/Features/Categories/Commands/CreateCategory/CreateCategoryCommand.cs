using TargetSettingTool.Application.Responses;
using MediatR;

namespace TargetSettingTool.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand: IRequest<Response<CreateCategoryDto>>
    {
        public string Name { get; set; }
    }
}
