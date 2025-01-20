using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;

namespace Ordering.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static IApplicationBuilder UseMigration(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.MigrateAsync();

            return applicationBuilder;
        }
    }
}
