using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

using TargetSettingTool.UI.Services;

namespace TargetSettingTool.UI.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUiServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<AddUsersFromExcelService>();

            return services;
        }
    }
}
