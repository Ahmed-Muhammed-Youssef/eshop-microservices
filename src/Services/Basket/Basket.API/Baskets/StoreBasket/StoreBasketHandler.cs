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

    public class StoreBasketHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart basket = await basketRepository.StoreBasket(command.Cart, cancellationToken);

            return new StoreBasketResult(basket.Username);
        }
    }
}
