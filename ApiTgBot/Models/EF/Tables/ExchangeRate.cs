namespace ApiTgBot.Models.EF.Tables
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        public CurrencyType GetCurrency { get; set; }
        public CurrencyType SetCurrency { get; set; }
        public decimal Rate { get; set; }
    }
}
