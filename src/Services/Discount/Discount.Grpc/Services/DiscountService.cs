using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : Discount.DiscountBase
    {
        public override async Task<CouponModel> GetDiscount(ProductKey request, ServerCallContext context)
        {
            Coupon? coupon = await dbContext.Coupons.Where(c => c.ProductName == request.ProductName).FirstOrDefaultAsync()
                ?? new Coupon() { ProductName="No Discount", Amount = 0, Description = "No Discount"};
            logger.LogInformation("Discount is retrieved for ProductName {productName}", coupon.ProductName);

            CouponModel response = new() { 
                Id = coupon.Id,
                ProductName = coupon.ProductName,
                Amount = coupon.Amount,
                Description = coupon.Description 
            };
            return response;
        }
        public override async Task<CouponModel> CreateDiscount(CouponModel request, ServerCallContext context)
        {
            Coupon coupon = new()
            {
                Id = request.Id,
                ProductName = request.ProductName,
                Description = request.Description,
                Amount = request.Amount 
            };

            dbContext.Coupons.Add(coupon);
            await dbContext.SaveChangesAsync();
            logger.LogInformation("Discount is created for ProductName {productName} with Amount {amount}", coupon.ProductName, coupon.Amount);

            return request;
        }
        public override async Task<CouponModel> UpdateDiscount(CouponModel request, ServerCallContext context)
        {
            Coupon coupon = dbContext.Coupons.FirstOrDefault(c => c.ProductName == request.ProductName) ?? throw new RpcException(new Status(StatusCode.NotFound, "No discount found"));
            
            coupon.ProductName = request.ProductName;
            coupon.Description = request.Description;
            coupon.Amount = request.Amount;

            dbContext.Entry(coupon).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is updated for ProductName {productName} with Amount {amount}", coupon.ProductName, coupon.Amount);

            return request;
        }
        public override Task<DeleteDiscountResponse> DeleteDiscount(ProductKey request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }
    }
}
