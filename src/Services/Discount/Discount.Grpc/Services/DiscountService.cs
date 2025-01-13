using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : Discount.DiscountBase
    {
        public override Task<CouponModel> GetDiscount(ProductKey request, ServerCallContext context)
        {
            return base.GetDiscount(request, context);
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
