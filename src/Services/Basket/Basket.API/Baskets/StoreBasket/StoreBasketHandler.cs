
namespace Basket.API.Baskets.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string Username);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(c => c.Cart).NotNull().WithMessage("Caer can not be null");
            RuleFor(c => c.Cart.Username).NotEmpty().WithMessage("Username is required");
        }
    }

    public class StoreBasketHandler(IBasketRepository basketRepository, Discount.Grpc.Discount.DiscountClient discountClient) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.Cart, cancellationToken);

            ShoppingCart basket = await basketRepository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(basket.Username);
        }

        private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                Discount.Grpc.CouponModel coupon = await discountClient.GetDiscountAsync(new Discount.Grpc.ProductKey() { ProductName = item.ProductName }, cancellationToken: cancellationToken);

                item.Price -= coupon.Amount;
            }
        }
    }
}
