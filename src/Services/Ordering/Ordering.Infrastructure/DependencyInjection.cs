using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors( sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

            return services;
        }
    }
}
