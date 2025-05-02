using BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
    {
        public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            long count = await dbContext.Orders
               .AsNoTracking()
               .LongCountAsync(cancellationToken);

            List<OrderDto> orders = await dbContext.Orders
               .Include(o => o.OrderItems)
               .AsNoTracking()
               .OrderBy(o => o.OrderName)
               .Skip(request.PaginationRequest.PageIndex * request.PaginationRequest.PageSize)
               .Take(request.PaginationRequest.PageSize)
               .Select(o => o.ToOrderDto())
               .ToListAsync(cancellationToken);

            PaginatedResult<OrderDto> paginatedOrders = new(request.PaginationRequest.PageIndex, request.PaginationRequest.PageSize, count, orders);
            return new GetOrdersQueryResult(paginatedOrders);
        }
    }
}
