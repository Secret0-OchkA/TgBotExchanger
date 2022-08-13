using ApiTgBot.Models.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace ApiTgBot.Models.EF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Currency> Currencies { get; set; } = null!;
        public DbSet<ExchangeRate> ExchangeRates { get; set; } = null!;
        public DbSet<Transfer> Transfers { get; set; } = null!;
        public DbSet<TransferArgument> arguments { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().Property(c => c.Amount).HasColumnType("money");

            modelBuilder.Entity<ExchangeRate>().Property(e => e.Rate).HasColumnType("money");

            modelBuilder.Entity<Transfer>().Property(t => t.Number).IsRequired();
            modelBuilder.Entity<Transfer>().Property(t => t.Code).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
    }
}
