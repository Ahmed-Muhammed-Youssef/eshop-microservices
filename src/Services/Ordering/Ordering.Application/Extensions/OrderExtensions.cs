namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static OrderDto ToOrderDto(this Order order) 
        {
            AddressDto billingAddressDto = new(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress ?? "", order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode);
            AddressDto shippingAddressDto = new(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress ?? "", order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode);
            PaymentDto paymentDto = new(order.Payment.CardName ?? "", order.Payment.CardNumber, order.Payment.Expiration, order.Payment.Cvv, order.Payment.PaymentMethod);
            List<OrderItemDto> orderItems = order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId, oi.ProductId, oi.Quantity, oi.Price)).ToList();
            return new(order.Id, order.CustomerId, order.OrderName, shippingAddressDto, billingAddressDto, paymentDto, order.Status, orderItems);
        }
    }
}
