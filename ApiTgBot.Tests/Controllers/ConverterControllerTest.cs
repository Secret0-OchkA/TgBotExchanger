using ApiTgBot.Controllers;
using ApiTgBot.Models.EF;
using Microsoft.EntityFrameworkCore;

namespace ApiTgBot.Tests.Controllers
{
    internal class ConverterControllerTest
    {
        IConvert converter;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName:"ApplicationListDatabase")
                .Options;
            var db = new ApplicationContext(options);

            ExchangeRate btc_usd = new ExchangeRate();
            btc_usd.SetCurrency = CurrencyType.BTC;
            btc_usd.GetCurrency = CurrencyType.USD;
            btc_usd.Rate = 2;

            db.ExchangeRates.Add(btc_usd);

            db.SaveChanges();

            converter = new ConverterController(db);
        }
    }
}
