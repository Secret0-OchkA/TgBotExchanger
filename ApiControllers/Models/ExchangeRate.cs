﻿namespace ApiControllers.Models
{
    public class ExchangeRate
    {
        public int Id { get; set; } = 0;
        public CurrencyType inType { get; set; } = CurrencyType.NotCurrency;
        public CurrencyType outType { get; set; } = CurrencyType.NotCurrency;
        public decimal Rate { get; set; } = 1;
    }
}