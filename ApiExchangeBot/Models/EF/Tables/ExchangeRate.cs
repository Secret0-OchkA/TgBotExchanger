namespace ApiExchangeBot.Models.EF.Tables
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        /// <summary>
        /// More valuable
        /// </summary>
        public CurrencyType inType { get; set; } = CurrencyType.NotCurrency;
        /// <summary>
        /// Less valuable
        /// </summary>
        public CurrencyType outType { get; set; } = CurrencyType.NotCurrency;
        public decimal Rate { get; set; } = 1;

        public Currency Convert(Currency currency)
        {
            if (currency.Type != inType && currency.Type != outType)
                throw new ArgumentException("Not type for convert");

            Currency result = new();
            if (currency.Type == inType)
            {
                result.Type = outType;
                result.Amount = currency.Amount * Rate;
            }
            else
            {
                result.Type = inType;
                result.Amount = currency.Amount / Rate;
            }

            return result;
        }
    }
}