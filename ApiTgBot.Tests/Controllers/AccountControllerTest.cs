using ApiExchangeBot.Controllers;
using ApiExchangeBot.Models.EF;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace ApiExchangeBot.Tests.Controllers
{
    internal class AccountControllerTest
    {
        public static void AreEqualByJson(object expected, object actual)
        {
            string expectedJson = JsonSerializer.Serialize(expected);
            string actualJson = JsonSerializer.Serialize(actual);
            Assert.AreEqual(expectedJson, actualJson);
        }
        ApplicationContext db;
        AccountController controller;
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

                db.Accounts.Add(account);
                db.ExchangeRates.Add(BTC_USD);

                db.SaveChanges();
            }

            db = factory.CreateDbContext(Array.Empty<string>());
            controller = new AccountController(db);
        }
        #region public IActionResult Get(id)
        [Test]
        public void Should_AccTest1_When_Id111111()
        {
            OkObjectResult? actual = controller.Get(111111) as OkObjectResult;
            Account? expect = (from a in db.Accounts
                               where a.TelegramId == 111111
                               select a).FirstOrDefault();

            Assert.IsNotNull(expect);
            Assert.IsNotNull(actual);
            AreEqualByJson(expect, actual.Value);
        }
        [Test]
        public void Should_RetNullAcc_When_NotExistId()
        {
            OkObjectResult? actual = controller.Get(-111) as OkObjectResult;
            Account expect = new Account();

            Assert.IsNotNull(expect);
            Assert.IsNotNull(actual);
            AreEqualByJson(expect, actual.Value);
        }
        #endregion
        # region public IActionResult Post(id,name)
        [TestCase(0,"name")]
        [TestCase(10,"")]
        [TestCase(22,null)]
        public void Should_BadRequest_When_OnParam(int id, string name)
        {
            IActionResult actual = controller.Post(id, name);

            Assert.IsTrue(actual.GetType() == typeof(BadRequestObjectResult));
        }
        [Test]
        public void Should_AddAcc_When_AllOk()
        {
            CreatedResult? actual = controller.Post(222222,"CreateAcc") as CreatedResult;
            Account? expect = (from a in db.Accounts
                               where a.TelegramId == 222222
                               select a).FirstOrDefault();

            Assert.IsNotNull(expect);
            Assert.IsNotNull(actual);
            AreEqualByJson(expect, actual.Value);
        }
        #endregion
        #region public IActionResult Delete(id)
        [Test]
        public void Should_Delete_When_ExistAcc()
        {
            controller.Delete(111111);

            Account? expect = (from a in db.Accounts
                               where a.TelegramId == 111111
                               select a).FirstOrDefault();
            Assert.IsNull(expect);
        }
        [Test]
        public void Should_BadRequest_When_NotAcc()
        {
            IActionResult actual = controller.Delete(-100);

            Assert.IsTrue(actual.GetType() == typeof(BadRequestResult));
        }
        #endregion
        #region public IActionResult Deposit(id,currency,number,code)
        [Test]
        public void Should_AddTransfer_WhenExistAcc()
        {
            Currency money = new Currency
            {
                Amount = 20,
                Type = CurrencyType.USD,
            };
            OkObjectResult? actual = controller.Deposit(111111,money,"123456","1234") as OkObjectResult;

            Transfer? expect = (from t in db.Transfers
                               where t.ownerTransfer.TelegramId == 111111
                               select t).FirstOrDefault();

            Assert.IsNotNull(actual);
            Assert.IsNotNull(expect);
            AreEqualByJson(expect, actual.Value);
        }

        public static IEnumerable<TestCaseData> SHould_BadRequest_When_NoParamTestCase
        {
            get
            {
                yield return new TestCaseData(-100, new Currency { Amount = 20, Type = CurrencyType.USD }, "123456", "1234");
                yield return new TestCaseData(111111, null, "123456", "1234");
                yield return new TestCaseData(111111, new Currency { Type = CurrencyType.USD }, "123456", "1234");
                yield return new TestCaseData(111111, new Currency { Amount = 20 }, "123456", "1234");
                yield return new TestCaseData(111111, new Currency { Amount = 20, Type = CurrencyType.USD }, "", "1234");
                yield return new TestCaseData(111111, new Currency { Amount = 20, Type = CurrencyType.USD }, null, "1234");
                yield return new TestCaseData(111111, new Currency { Amount = 20, Type = CurrencyType.USD }, "123456", "");
                yield return new TestCaseData(111111, new Currency { Amount = 20, Type = CurrencyType.USD }, "123456", null);
                yield return new TestCaseData(111111, new Currency { Amount = -20, Type = CurrencyType.USD }, "123456", null);
            }
        }
        [TestCaseSource("SHould_BadRequest_When_NoParamTestCase")]
        public void SHould_BadRequest_When_NoParam(int id, Currency money, string? number, string? code)
        {
            BadRequestResult? actual = controller.Deposit(id, money, number,code) as BadRequestResult;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.GetType() == typeof(BadRequestResult));
        }
        #endregion
        #region IActionResult Withdraw(id,currency)
        [Test]
        public void Should_Withdraw_When_Ok()
        {
            Currency money = new Currency
            {
                Amount = 10,
                Type = CurrencyType.USD,
            };
            OkObjectResult? result = controller.Withdraw(111111, money) as OkObjectResult;

            Account? actual = (from a in db.Accounts
                                where a.TelegramId == 111111
                                select a).FirstOrDefault();

            Currency? currency = actual?.Wallet.First();

            Assert.IsNotNull(result);
            Assert.IsNotNull(actual);
            AreEqualByJson(new {Type = CurrencyType.USD, Amount = 10 }, new {Type = currency.Type,Amount = currency.Amount});
        }
        public static IEnumerable<TestCaseData> Should_BadRequest_When_NotParamTestCase
        {
            get
            {
                yield return new TestCaseData(-100, new Currency { Amount = 20, Type = CurrencyType.USD });
                yield return new TestCaseData(111111, null);
                yield return new TestCaseData(111111, new Currency { Type = CurrencyType.USD });
                yield return new TestCaseData(111111, new Currency { Amount = 20 });
                yield return new TestCaseData(111111, new Currency { Amount = -20, Type = CurrencyType.USD });
            }
        }
        [TestCaseSource("Should_BadRequest_When_NotParamTestCase")]
        public void Should_BadRequest_When_NotParam(int id, Currency? money)
        {
            BadRequestObjectResult? actual = controller.Withdraw(id, money) as BadRequestObjectResult;

            Assert.IsNotNull(actual);
            Assert.IsTrue(actual.GetType() == typeof(BadRequestObjectResult));
        }
        #endregion
    }
}
