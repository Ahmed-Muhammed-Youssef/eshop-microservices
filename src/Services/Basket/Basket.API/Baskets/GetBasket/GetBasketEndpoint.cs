namespace Basket.API.Baskets.GetBasket
{
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{username}", async (string username, ISender sender) =>
            {
                GetBasketResult result = await sender.Send(new GetBasketQuery(username));
                GetBasketResponse response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);

            })
            .WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get basket by username")
            .WithDescription("Get basket by username");
        }
    }
}
