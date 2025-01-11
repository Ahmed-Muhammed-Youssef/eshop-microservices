namespace Basket.API.Baskets.DeleteBasket
{
    public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required");
        }
    }
    public class DeleteBasketHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var isSuccess = await basketRepository.DeleteBasket(request.Username, cancellationToken);

            return new DeleteBasketResult(isSuccess);
        }
    }
}
