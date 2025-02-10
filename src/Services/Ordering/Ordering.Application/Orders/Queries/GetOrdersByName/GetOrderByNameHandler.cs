using Microsoft.EntityFrameworkCore;

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
                .Select(o => ProjectToOrderDto(o))
                .OrderBy(o => o.OrderName)
                .ToListAsync(cancellationToken);

            return new GetOrderByNameQueryResult(orders);
        }

        private OrderDto ProjectToOrderDto(Order order)
        {
            AddressDto billingAddressDto = new(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode);
            AddressDto shippingAddressDto = new(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode);
            PaymentDto paymentDto = new(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.Cvv, order.Payment.PaymentMethod);
            List<OrderItemDto> orderItems = order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList();
            return new(order.Id, order.CustomerId, order.OrderName, shippingAddressDto, billingAddressDto, paymentDto, order.Status, orderItems);
        }
    }
}
