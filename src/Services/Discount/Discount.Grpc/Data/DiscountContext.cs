using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DiscountContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon() { Id = 1, ProductName = "IPhone 12", Description = "a Mobile phone", Amount = 20 },
                new Coupon() { Id = 2, ProductName = "S22", Description = "a Mobile phone", Amount = 20 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
