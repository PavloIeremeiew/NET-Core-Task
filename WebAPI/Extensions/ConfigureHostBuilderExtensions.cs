using Serilog;

namespace NET_Core_Task.WebAPI.Extensions
{
    public static class ConfigureHostBuilderExtensions
    {
        public static void ConfigureSerilog(this IServiceCollection services, WebApplicationBuilder builder)
        {
            builder.Host.UseSerilog((ctx, services, loggerConfiguration) =>
            {
                loggerConfiguration
                    .ReadFrom.Configuration(builder.Configuration);
            });
        }
    }
}
