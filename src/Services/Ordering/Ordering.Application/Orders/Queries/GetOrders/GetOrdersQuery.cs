using BuildingBlocks.Pagination;
using FluentValidation;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQueryResult(PaginatedResult<OrderDto> Orders);
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersQueryResult>;
    public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
    {
        public GetOrdersQueryValidator()
        {
                
        }
    }
}
