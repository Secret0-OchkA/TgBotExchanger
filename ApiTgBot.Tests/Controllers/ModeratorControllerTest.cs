using ApiExchangeBot.Controllers;
using ApiExchangeBot.Models.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiExchangeBot.Tests.Controllers
{
    internal class ModeratorControllerTest
    {
        public static void AreEqualByJson(object expected, object actual)
        {
            string expectedJson = JsonSerializer.Serialize(expected);
            string actualJson = JsonSerializer.Serialize(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }
        ApplicationContext db;
        ModeratorController controller;
        [SetUp]
        public void Setup()
        {
            ApplicationContextFactoryInMemory factory = new ApplicationContextFactoryInMemory();
            using(ApplicationContext db = factory.CreateDbContext(Array.Empty<string>()))
            {
                Currency currencyAcc = new Currency()
                {
                    Amount = 20,
                    Type = CurrencyType.USD,
                };
                ExchangeRate BTC_USD = new ExchangeRate()
                {
                    inType = CurrencyType.BTC,
                    outType = CurrencyType.USD,
                    Rate = 2
                };

                Account account = new Account()
                {
                    TelegramId = 111111,
                    Name = "test1",
                    Wallet = new List<Currency>()
                    {
                        currencyAcc,
                    }
                };

                Currency usdMoney = new Currency
                {
                    Amount = 10,
                    Type = CurrencyType.USD,
                };
                Currency btcMoney = new Currency
                {
                    Amount = 10,
                    Type = CurrencyType.BTC,
                };
                Transfer usdTransfer = new Transfer
                {
                    ownerTransfer = account,
                    Number = "111111",
                    Code = "1111",
                    Amount = usdMoney,
                    arguments = new()
                    {
                        new TransferArgument
                        {
                            Type = TransferArgumentType.Document,
                            FileId = "docUsd"
                        },
                        new TransferArgument
                        {
                            Type = TransferArgumentType.Photo,
                            FileId = "photoUsd"
                        },
                    }
                };
                Transfer btcTransfer = new Transfer
                {
                    ownerTransfer = account,
                    Number = "222222",
                    Code = "2222",
                    Amount = btcMoney,
                    arguments = new()
                    {
                        new TransferArgument
                        {
                            Type = TransferArgumentType.Document,
                            FileId = "docBtc"
                        },
                        new TransferArgument
                        {
                            Type = TransferArgumentType.Photo,
                            FileId = "photoBtc"
                        },
                    }
                };

                db.Accounts.Add(account);
                db.ExchangeRates.Add(BTC_USD);

                db.Transfers.Add(usdTransfer);
                db.Transfers.Add(btcTransfer);

                db.SaveChanges();
            }

            db = factory.CreateDbContext(Array.Empty<string>());
            controller = new ModeratorController(db);
        }
        #region IActionResult Get()
        [Test]
        public void Should_ReturnAllTransfers_When_Get()
        {
            OkObjectResult? ok = controller.Get() as OkObjectResult;

            List<Transfer> transfers = (from t in db.Transfers
                                            .Include(t => t.ownerTransfer)
                                               .ThenInclude(a => a.Wallet)
                                            .Include(t => t.Amount)
                                            .Include(t => t.arguments)
                                        select t).ToList();

            Assert.IsNotNull(ok);
            AreEqualByJson(transfers, ok.Value);
        }
        #endregion
        #region IActionResult Get(id)
        [Test]
        public void Should_GetTransfer_When_Exist()
        {
            OkObjectResult? ok = controller.Get(1) as OkObjectResult;

            Transfer transfer = (from t in db.Transfers
                                    .Include(t => t.ownerTransfer)
                                       .ThenInclude(a => a.Wallet)
                                    .Include(t => t.Amount)
                                    .Include(t => t.arguments)
                                 where t.Id == 1
                                 select t).First();

            Assert.IsNotNull(ok);
            AreEqualByJson(ok.Value, transfer);
        }
        [Test]
        public void Should_GetNullTransfer_When_NotExist()
        {
            OkObjectResult? ok = controller.Get(-1) as OkObjectResult;
            Assert.IsNotNull(ok);
            AreEqualByJson(ok.Value, new Transfer());
        }
        #endregion
        #region IActionResult ConfirmTransfer(id)
        [TestCase(1)]
        [TestCase(2)]
        public void Should_AddMoney_When_Confirm(int id)
        {
            OkObjectResult? ok = controller.ConfirmTransfer(id) as OkObjectResult;
            Assert.IsNotNull(ok);

            Account? account = ok.Value as Account;
            Assert.IsNotNull(account);

            Account? dbAcc = (from a in db.Accounts
                                .Include(a => a.Wallet)
                             where a.TelegramId == 111111
                             select a).FirstOrDefault();
            Assert.IsNotNull(dbAcc);

            AreEqualByJson(dbAcc, account);
        }
        [Test]
        public void Should_BadRequest_When_NotExistTransfer()
        {
            BadRequestResult bad = controller.ConfirmTransfer(-1) as BadRequestResult;
            Assert.IsNotNull(bad);
        }
        #endregion
        #region IActionResult Delete(id)
        [Test]
        public void Should_Delete_When_Exist()
        {
            OkObjectResult? ok = controller.Delete(1) as OkObjectResult;
            Assert.IsNotNull(ok);

            Transfer? transferDb = (from t in db.Transfers
                                   where t.Id == 1
                                   select t).FirstOrDefault();
            Assert.IsNull(transferDb);
        }
        [Test]
        public void Should_BadRequest_When_NotExist()
        {
            BadRequestResult? bad = controller.Delete(-1) as BadRequestResult;
            Assert.IsNotNull(bad);
        }
        #endregion
    }
}
