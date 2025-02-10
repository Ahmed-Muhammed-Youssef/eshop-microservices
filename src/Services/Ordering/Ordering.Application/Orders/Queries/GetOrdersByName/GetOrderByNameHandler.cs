using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public class GetOrderByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrderByNameQuery, GetOrderByNameQueryResult>
    {
        public async Task<GetOrderByNameQueryResult> Handle(GetOrderByNameQuery request, CancellationToken cancellationToken)
        {
            List<OrderDto> orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.OrderName.Contains(request.Name))
                .Select(o => o.ToOrderDto())
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

            return new GetOrderByNameQueryResult(orders);
        }
    }
}
