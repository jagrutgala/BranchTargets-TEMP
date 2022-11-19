using TargetSettingTool.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TargetSettingTool.Application.Contracts.Persistence
{
    public interface ICategoryRepository : IAsyncRepository<Category>
    {
        Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents);
        Task<Category> AddCategory(Category category);
    }
}
