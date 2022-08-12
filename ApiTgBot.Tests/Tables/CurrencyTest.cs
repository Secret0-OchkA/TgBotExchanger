namespace ApiTgBot.Tests.Tables
{
    internal class CurrencyTest
    {
        Currency a_USD;
        Currency b_USD;
        Currency c_BTC;
        Currency zero;
        [SetUp]
        public void Setup()
        {
            a_USD = new Currency();
            a_USD.Amount = 5;
            a_USD.Type = CurrencyType.USD;

            b_USD = new Currency();
            b_USD.Amount = 7;
            b_USD.Type = CurrencyType.USD;

            c_BTC = new Currency();
            c_BTC.Amount = 8;
            c_BTC.Type = CurrencyType.BTC;

            zero = new();
            zero.Amount = 0;
            zero.Type = CurrencyType.USD;
        }

        #region operator+
        [Test]
        public void Should_AddAmount_When_MatchType()
        {
            Currency result = a_USD + b_USD;

            Assert.IsTrue(result.Type == CurrencyType.USD && result.Amount == 12);
        }

        [Test]
        public void Should_ThrowExc_When_TypeNoMatch()
        {
            Assert.That(() => a_USD + c_BTC, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Should_ReturnA_When_AmountBZero()
        {
            Currency result = a_USD + zero;
            Assert.IsTrue(result == a_USD);
        }
        [Test]
        public void Should_ReturnB_When_AmountAZero()
        {
            Currency result = zero + a_USD;
            Assert.IsTrue(result == a_USD);
        }
        #endregion

        #region operator-
        [Test]
        public void Should_SubAmount_When_MatchType()
        {
            Currency result = b_USD - a_USD;

            Assert.IsTrue(result.Type == CurrencyType.USD && result.Amount == 2);
        }

        [Test]
        public void Should_SubThrowExc_When_TypeNoMatch()
        {
            Assert.That(() => a_USD - c_BTC, Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void Should_SubReturnA_When_AmountBZero()
        {
            Currency result = a_USD - zero;
            Assert.IsTrue(result == a_USD);
        }
        #endregion
    }
}
