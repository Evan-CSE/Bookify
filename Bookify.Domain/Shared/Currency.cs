namespace Bookify.Domain.Shared
{
    public record Currency
    {
        public static readonly Currency Bdt = new("bdt");
        internal static readonly Currency None = new("");

        private Currency(string code) => Code = code;

        public static Currency FromCode(string code)
        {
            return CurrencyCodes.FirstOrDefault(c => c.Code == code) ??
                throw new ApplicationException("Invalid Currency provided");
        }

        public static readonly IReadOnlyCollection<Currency> CurrencyCodes = [Bdt];

        public string Code { get; init; }
    }
}
