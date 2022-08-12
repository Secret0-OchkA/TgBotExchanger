namespace ApiTgBot.Tests.Tables
{
    internal class ExchangeRateTest
    {
        ExchangeRate exchangeRate;

        [SetUp]
        public void Setup()
        {
            exchangeRate = new();
            exchangeRate.SetCurrency = CurrencyType.BTC;
            exchangeRate.GetCurrency = CurrencyType.USD;
            exchangeRate.Rate = 2;
        }

        [TestCase(CurrencyType.USD, CurrencyType.BTC)]
        [TestCase(CurrencyType.BTC, CurrencyType.USD)]
        public void Should_GetType_When_InOtherType(CurrencyType type, CurrencyType expectType)
        {
            Currency currency = new();
            currency.Type = type;
            currency.Amount = 3;

            Currency result = exchangeRate.Convert(currency);

            Assert.IsTrue(result.Type == expectType);
        }

        [Test]
        public void Should_GetMultRes_When_InRare() //10BTC -> 20USD
        {
            Currency currency = new();
            currency.Type = CurrencyType.BTC;
            currency.Amount = 10;

            Currency result = exchangeRate.Convert(currency);

            Assert.IsTrue(result.Amount == 20);
        }
        [Test]
        public void Should_Div_When_InvirtType() // 10USD -> 5BTC
        {
            Currency currency = new();
            currency.Type = CurrencyType.USD;
            currency.Amount = 10;

            Currency result = exchangeRate.Convert(currency);

            Assert.IsTrue(result.Amount == 5);
        }

        [Test]
        public void Should_Throw_When_NoConvert()
        {
            Currency currency = new();
            currency.Type = CurrencyType.RUB;
            currency.Amount = 2;

            Assert.That(() => exchangeRate.Convert(currency), Throws.TypeOf<ArgumentException>());
        }
    }
}
