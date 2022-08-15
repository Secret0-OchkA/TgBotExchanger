using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiExchangeBot.Models.EF
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder();
            configBuilder.SetBasePath(Directory.GetCurrentDirectory());
            configBuilder.AddJsonFile("appsettings.json");

            string connect = configBuilder.Build().GetConnectionString("DefaultConnection");

            var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            DbContextOptions<ApplicationContext> options = optionBuilder.UseSqlServer(connect).Options;

            return new ApplicationContext(options);
        }
    }
}
