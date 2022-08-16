using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiExchangeBot.Models.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ApiExchangeBot.Tests
{
    internal class ApplicationContextFactoryInMemory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            DbContextOptions<ApplicationContext> options = optionBuilder.UseInMemoryDatabase("MemoryDatabase").Options;

            return new ApplicationContext(options);
        }
    }
}
