using TargetSettingTool.Application.Features.Events.Queries.GetEventsExport;
using System.Collections.Generic;

namespace TargetSettingTool.Application.Contracts.Infrastructure
{
    public interface ICsvExporter
    {
        byte[] ExportEventsToCsv(List<EventExportDto> eventExportDtos);
    }
}
