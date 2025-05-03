namespace BuildingBlocks.Messaging.Events
{
    public record BasketCheckoutEvent : IntegrationEvent
    {
        public string UserName { get; set; } = string.Empty;
        public Guid CustomerId { get; set; }
        // Shipping and Billing Address
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string AddressLine { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        // Payment
        public string CardName { get; set; } = string.Empty;
        public string Cvv { get; set; } = string.Empty;
        public string CardNumber { get; set; } = string.Empty;
        public string Expiration { get; set; } = string.Empty;
        public int PaymentMethod { get; set; }
    }
}
