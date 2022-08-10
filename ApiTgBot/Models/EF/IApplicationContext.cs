using ApiTgBot.Models.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace ApiTgBot.Models.EF
{
    public interface IApplicationContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}
