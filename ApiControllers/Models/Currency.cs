namespace ApiControllers.Models
{ 
    public class Currency
    {
        public int Id { get; set; } = 0;
        public CurrencyType Type { get; set; } = CurrencyType.NotCurrency;
        public decimal Amount { get; set; } = 0;
    }

    public enum CurrencyType
    {
        NotCurrency,
        RUB,
        BTC,
        USD,
    }
}
