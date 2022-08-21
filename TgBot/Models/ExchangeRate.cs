namespace TgBot.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public CurrencyType inType { get; set; } = CurrencyType.NotCurrency;
        public CurrencyType outType { get; set; } = CurrencyType.NotCurrency;
        public decimal Rate { get; set; } = 1;
    }
}