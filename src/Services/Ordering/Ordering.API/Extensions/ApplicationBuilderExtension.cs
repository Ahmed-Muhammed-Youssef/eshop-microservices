using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Data;

namespace Ordering.API.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> InitializeDatabaseAsync(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();
            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();
            await SeedAsync(dbContext);
            return applicationBuilder;
        }
        private static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            await SeedCustomersAsync(dbContext);
        }

        private static async Task SeedCustomersAsync(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Customers.AnyAsync()) 
            {
                await dbContext.Customers.AddRangeAsync(InitialData.Customers);
                await dbContext.SaveChangesAsync();
            }

        }
    }
}
