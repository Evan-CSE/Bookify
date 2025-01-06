namespace Bookify.Domain.Shared
{
    public record Money(decimal Amount, Currency Currency)
    {
        public static Money operator +(Money firstAmount, Money secondAmount)
        {
            if (firstAmount.Currency != secondAmount.Currency)
            {
                throw new InvalidOperationException("Currency should be identical for both amount");
            }
            return new Money(firstAmount.Amount + secondAmount.Amount, firstAmount.Currency);
        }

        public static Money Zero() => new(0, Currency.None);

        public static Money Zero(Currency currency) => new Money(0, currency);

        public bool IsZero() => this == Zero(Currency);
    }
}
