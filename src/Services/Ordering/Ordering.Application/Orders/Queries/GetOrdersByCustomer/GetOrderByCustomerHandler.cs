﻿using Microsoft.EntityFrameworkCore;
using Ordering.Application.Extensions;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrderByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrderByCustomerQueryResult>
    {
        public async Task<GetOrderByCustomerQueryResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
        {
            List<OrderDto> orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == request.CustomerId)
                .OrderBy(o => o.OrderName)
                .Select(o => o.ToOrderDto())
                .ToListAsync(cancellationToken);

            return new GetOrderByCustomerQueryResult(orders);
        }
    }
}
