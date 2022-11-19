using TargetSettingTool.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface IEventRepository : IAsyncRepository<Event>
    {
        Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate);
        Task<Event> AddEventWithCategory(Event @event);
    }
}
