namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        private Payment(string? cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            Cvv = cvv;
            PaymentMethod = paymentMethod;
        }

        public string? CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string Cvv { get; } = default!;
        public int PaymentMethod { get; }

        public static Payment Of(string? cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName, nameof(cardName));
            ArgumentException.ThrowIfNullOrWhiteSpace(expiration, nameof(expiration));
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv, nameof(cvv));
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

            return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
        }
    }
}
