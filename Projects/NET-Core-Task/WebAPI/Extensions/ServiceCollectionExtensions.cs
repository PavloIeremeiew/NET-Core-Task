using FluentValidation;
using MediatR;
using NET_Core_Task.BLL.Services.Logger;
using NET_Core_Task.DAL.Repositories.Interfaces.Base;
using NET_Core_Task.DAL.Repositories.Realizations.Base;

namespace NET_Core_Task.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddRepositoryServices();
            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddAutoMapper(currentAssemblies);
            services.AddValidatorsFromAssemblies(currentAssemblies);
            services.AddMediatR(currentAssemblies);
            services.AddScoped<ILoggerService, LoggerService>();
        }
    }
}
