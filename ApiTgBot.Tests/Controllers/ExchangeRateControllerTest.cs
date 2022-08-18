using ApiExchangeBot.Controllers;
using ApiExchangeBot.Models.EF;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiExchangeBot.Tests.Controllers
{
    internal class ExchangeRateControllerTest
    {
        public static void AreEqualByJson(object expected, object actual)
        {
            string expectedJson = JsonSerializer.Serialize(expected);
            string actualJson = JsonSerializer.Serialize(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }
        ApplicationContext db;
        ExchangeRateController controller;
        [SetUp]
        public void Setup()
        {
            ApplicationContextFactoryInMemory factory = new ApplicationContextFactoryInMemory();
            using(ApplicationContext db = factory.CreateDbContext(Array.Empty<string>()))
            {
                ExchangeRate BTC_USD = new ExchangeRate()
                {
                    inType = CurrencyType.BTC,
                    outType = CurrencyType.USD,
                    Rate = 2
                };

                Currency currencyUsd = new Currency()
                {
                    Amount = 20,
                    Type = CurrencyType.USD,
                };
                Account accountUsd = new Account()
                {
                    TelegramId = 111111,
                    Name = "UsdAcc",
                    Wallet = new List<Currency>()
                    {
                        currencyUsd,
                    }
                };

                Currency currencyBtc = new Currency()
                {
                    Amount = 20,
                    Type = CurrencyType.BTC,
                };

                Account accountBtc = new Account()
                {
                    TelegramId = 222222,
                    Name = "BtcAcc",
                    Wallet = new List<Currency>()
                    {
                        currencyBtc,
                    }
                };

                db.Accounts.Add(accountUsd);
                db.Accounts.Add(accountBtc);
                db.ExchangeRates.Add(BTC_USD);

                db.SaveChanges();
            }

            db = factory.CreateDbContext(Array.Empty<string>());
            controller = new ExchangeRateController(db);
        }
        #region IActionResult Convert(id,inType,amount,outType)
        [TestCase(CurrencyType.NotCurrency, 5, CurrencyType.BTC)]
        [TestCase(CurrencyType.USD, 5, CurrencyType.NotCurrency)]
        [TestCase(CurrencyType.USD, -10, CurrencyType.NotCurrency)]
        public void Should_BadReques_When_NullTypeOrIncorrect(CurrencyType inType, decimal amount, CurrencyType outType)
        {
            BadRequestResult? bad = controller.Convert(111111, inType, amount, outType) as BadRequestResult;
            Assert.IsNotNull(bad);
        }
        #endregion
    }
}
