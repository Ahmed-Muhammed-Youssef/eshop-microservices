
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    public record GetOrdersByCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{id}", async(Guid id, ISender sender) =>
            {
                GetOrdersByCustomerQuery query = new(id);

                GetOrderByCustomerQueryResult result = await sender.Send(query);

                return Results.Ok(result.Adapt<GetOrdersByCustomerResponse>());
            })
            .WithName("GetOrdersByCustomer")
            .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer"); ;
        }
    }
}
