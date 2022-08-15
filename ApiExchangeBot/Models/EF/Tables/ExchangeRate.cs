﻿namespace ApiExchangeBot.Models.EF.Tables
{
    public class ExchangeRate
    {
        public int Id { get; set; }
        /// <summary>
        /// More valuable
        /// </summary>
        public CurrencyType SetCurrency { get; set; } = CurrencyType.NotCurrency;//in  currency
        /// <summary>
        /// Less valuable
        /// </summary>
        public CurrencyType GetCurrency { get; set; } = CurrencyType.NotCurrency;//out currency
        public decimal Rate { get; set; } = 1;

        public Currency Convert(Currency currency)
        {
            if (currency.Type != SetCurrency && currency.Type != GetCurrency)
                throw new ArgumentException("Not type for convert");

            Currency result = new();
            if (currency.Type == SetCurrency)
            {
                result.Type = GetCurrency;
                result.Amount = currency.Amount * Rate;
            }
            else
            {
                result.Type = SetCurrency;
                result.Amount = currency.Amount / Rate;
            }

            return result;
        }
    }
}