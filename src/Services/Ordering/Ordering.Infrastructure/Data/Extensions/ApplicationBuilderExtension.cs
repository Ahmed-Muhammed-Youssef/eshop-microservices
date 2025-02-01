using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extensions
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
            await SeedProductsAsync(dbContext);
            await SeedOrderWithItemsAsync(dbContext);
        }

        private static async Task SeedCustomersAsync(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Customers.AnyAsync()) 
            {
                await dbContext.Customers.AddRangeAsync(InitialData.Customers);
                await dbContext.SaveChangesAsync();
            }

        }
        private static async Task SeedProductsAsync(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Products.AnyAsync())
            {
                await dbContext.Products.AddRangeAsync(InitialData.Products);
                await dbContext.SaveChangesAsync();
            }
        }
        private static async Task SeedOrderWithItemsAsync(ApplicationDbContext dbContext)
        {
            if (!await dbContext.Orders.AnyAsync())
            {
                await dbContext.Orders.AddRangeAsync(InitialData.OrdersWithItems);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
