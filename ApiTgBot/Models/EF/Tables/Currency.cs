namespace ApiTgBot.Models.EF.Tables
{
    public class Currency
    {
        public int Id { get; set; }
        public CurrencyType Type { get; set; }
        public decimal Amount { get; set; }

        public static Currency operator +(Currency a, Currency b)
        {
            if (a.Type != b.Type)
                throw new ArgumentException("Not match currency tupes");

            if (a.Amount == 0) return b;
            if (b.Amount == 0) return a;

            Currency result = new Currency();
            result.Amount = a.Amount + b.Amount;
            result.Type = a.Type;
            return result;
        }

        public static Currency operator -(Currency a, Currency b)
        {
            if (a.Type != b.Type)
                throw new ArgumentException("Not match currency tupes");

            if (a.Amount == 0) return b;
            if (b.Amount == 0) return a;

            Currency result = new Currency();
            result.Amount = a.Amount - b.Amount;
            result.Type = a.Type;
            return result;
        }
    }

    public enum CurrencyType
    {
        RUB,
        BTC,
        USD,
    }
}
