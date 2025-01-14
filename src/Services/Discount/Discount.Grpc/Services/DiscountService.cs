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
        public override Task<CouponModel> CreateDiscount(CouponModel request, ServerCallContext context)
        {
            return base.CreateDiscount(request, context);
        }
        public override Task<CouponModel> UpdateDiscount(CouponModel request, ServerCallContext context)
        {
            return base.UpdateDiscount(request, context);
        }
        public override Task<DeleteDiscountResponse> DeleteDiscount(ProductKey request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }
    }
}
