namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = CreateNewOrder(request.Order);

            await dbContext.Orders.AddAsync(order);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id);
        }

        private static Order CreateNewOrder(OrderDto dto)
        {
            Address shippingAddress = Address.Of(dto.ShippingAddress.FirstName, dto.ShippingAddress.LastName, dto.ShippingAddress.EmailAddress, dto.ShippingAddress.AddressLine, dto.ShippingAddress.Country, dto.ShippingAddress.State, dto.ShippingAddress.ZipCode);
            Address billingAddress = Address.Of(dto.BillingAddress.FirstName, dto.BillingAddress.LastName, dto.BillingAddress.EmailAddress, dto.BillingAddress.AddressLine, dto.BillingAddress.Country, dto.BillingAddress.State, dto.BillingAddress.ZipCode);
            Payment payment = Payment.Of(dto.Payment.CardName, dto.Payment.CardNumber, dto.Payment.Expiration, dto.Payment.Cvv, dto.Payment.PaymentMethod);
            Order order =  Order.Create(Guid.NewGuid(), dto.CustomerId, dto.OrderName, shippingAddress, billingAddress, payment);

            foreach (var orderItem in dto.OrderItems)
            {
                order.Add(orderItem.ProductId, orderItem.Quantity, orderItem.Price);
            }

            return order;
        }
    }
}
