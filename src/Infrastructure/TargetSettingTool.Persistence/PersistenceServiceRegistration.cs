using TargetSettingTool.Application.Contracts.Persistence;
using TargetSettingTool.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TargetSettingTool.Application.Services;

namespace TargetSettingTool.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ApplicationConnectionString")));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRightRepository, RightRepository>();
            services.AddScoped<IBranchRepository, BranchRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISideBarMenuRepository, SideBarMenuRepository>();
            services.AddScoped<IUserBranchRepository, UserBranchRepositry>();
            services.AddScoped<IParameterTypeRepository, ParameterTypeRepository>();
            services.AddScoped<IParameterRepository, ParameterRepository>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IBranchTargetRepository, BranchTargetRepository>();
            services.AddScoped<IUserParameterRepository, UserParameterRepository>();


            return services;
        }
    }
}
