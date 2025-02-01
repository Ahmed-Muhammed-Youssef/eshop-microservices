
using Ordering.Application.Exceptions;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderHandler (IApplicationDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderCommandResult>
    {
        public async Task<UpdateOrderCommandResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            Order order = await dbContext.Orders.FindAsync([request.Order.Id], cancellationToken) ?? throw new OrderNotFoundException(request.Order.Id);
            UpdateOrder(order, request.Order);

            dbContext.Orders.Update(order);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderCommandResult(true);
        }

        private static Order UpdateOrder(Order order, OrderDto dto)
        {
            Address shippingAddress = Address.Of(dto.ShippingAddress.FirstName, dto.ShippingAddress.LastName, dto.ShippingAddress.EmailAddress, dto.ShippingAddress.AddressLine, dto.ShippingAddress.Country, dto.ShippingAddress.State, dto.ShippingAddress.ZipCode);
            Address billingAddress = Address.Of(dto.BillingAddress.FirstName, dto.BillingAddress.LastName, dto.BillingAddress.EmailAddress, dto.BillingAddress.AddressLine, dto.BillingAddress.Country, dto.BillingAddress.State, dto.BillingAddress.ZipCode);
            Payment payment = Payment.Of(dto.Payment.CardName, dto.Payment.CardNumber, dto.Payment.Expiration, dto.Payment.Cvv, dto.Payment.PaymentMethod);
            order.Update(dto.OrderName, shippingAddress, billingAddress, payment, dto.Status);

            return order;
        }
    }
}
