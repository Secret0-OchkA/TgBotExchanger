namespace ApiTgBot.Models.EF.Tables
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public CurrencyType SetCurrency { get; set; } //in  currency
        public CurrencyType GetCurrency { get; set; } //out currency
        public decimal Rate { get; set; }
    }
}
