using TargetSettingTool.Application.Responses;
using MediatR;

namespace TargetSettingTool.Application.Features.Categories.Commands.StoredProcedure
{
    public class StoredProcedureCommand: IRequest<Response<StoredProcedureDto>>
    {
        public string Name { get; set; }
    }
}
