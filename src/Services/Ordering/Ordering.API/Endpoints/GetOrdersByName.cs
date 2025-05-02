using Ordering.Application.Orders.Queries.GetOrdersByName;

namespace Ordering.API.Endpoints
{
    public record GetOrdersByNameResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/{name}", async(string name, ISender sender) =>
            {
                GetOrderByNameQueryResult result = await sender.Send(new GetOrderByNameQuery(name));

                return Results.Ok(result.Adapt<GetOrdersByNameResponse>());
            })
            .WithName("GetOrdersByName")
            .Produces<GetOrdersByNameResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Order By Name")
            .WithDescription("Get Order By Name");
        }
    }
}
