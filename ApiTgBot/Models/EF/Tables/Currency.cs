using Microsoft.EntityFrameworkCore;

namespace ApiTgBot.Models.EF.Tables
{
    public class Currency
    {
        public int Id { get; set; }
        public CurrencyType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
