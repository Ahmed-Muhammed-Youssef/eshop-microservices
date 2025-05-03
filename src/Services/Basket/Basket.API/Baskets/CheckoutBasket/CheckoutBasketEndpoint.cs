using Basket.API.Dtos;

namespace Basket.API.Baskets.CheckoutBasket
{
    public record CheckoutBasketRequest(BasketCheckoutDto Basket);
    public record CheckoutBasketResponse(bool IsSuccess);
    public class CheckoutBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket/checkout", async (CheckoutBasketRequest request, ISender sender) =>
            {
                CheckoutBasketCommand command = request.Adapt<CheckoutBasketCommand>();
                CheckoutBasketResult result = await sender.Send(command);
                CheckoutBasketResponse response = result.Adapt<CheckoutBasketResponse>();
                return Results.Ok(result);
            })
            .WithName("CheckoutBasket")
            .Produces<CheckoutBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Checkout Basket")
            .WithDescription("Checkout Basket"); ;
        }
    }
}
