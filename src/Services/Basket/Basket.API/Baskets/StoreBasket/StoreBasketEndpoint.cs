using Basket.API.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.API.Baskets.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string Username);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) => {

                StoreBasketCommand command = request.Adapt<StoreBasketCommand>();

                StoreBasketResult result = await sender.Send(command);

                return Results.Ok(result);
            })
            .WithName("StoreBasket")
            .Produces<StoreBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store basket")
            .WithDescription("Store basket");
        }
    }
}
