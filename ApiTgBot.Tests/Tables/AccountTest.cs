namespace ApiTgBot.Tests.Tables
{
    internal class AccountTest
    {
        Account account;
        int currencyCount;
        [SetUp]
        public void Setup()
        {
            Currency a = new();
            a.Type = CurrencyType.USD;
            a.Amount = 5;

            account = new Account();
            account.Id = 0;
            account.Name = "TestAccount";
            account.Wallet = new()
            {
                a
            };

            currencyCount = account.Wallet.Count();
        }

        [Test]
        public void Should_AddCurrency_When_NewCurrencyTupe()
        {
            Currency currency = new();
            currency.Type = CurrencyType.BTC;
            currency.Amount = 0;

            account.Deposit(currency);

            Assert.IsTrue(currencyCount < account.Wallet.Count);
        }
        [Test]
        public void Should_AddCurrencyAmount_When_ContainsCurrencyTupe()
        {
            Currency currency = new();
            currency.Type = CurrencyType.USD;
            currency.Amount = 5;

            Currency result = account.Deposit(currency);

            Assert.IsTrue(currencyCount == account.Wallet.Count() && result.Amount == 10);
        }

        [Test]
        public void Should_SubCurrency_When_ContainsCurrencyType()
        {
            Currency currency = new();
            currency.Type = CurrencyType.USD;
            currency.Amount = 2;

            Currency result = account.Withdraw(currency);

            Assert.IsTrue(result.Amount == 3);
        }
        [Test]
        public void Should_ThrowExc_When_MoneySig()
        {
            Currency currency = new();
            currency.Amount = -9;
            currency.Type = CurrencyType.USD;

            Assert.That(() => account.Deposit(currency), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void Should_SubThrowExc_When_MoneySig()
        {
            Currency currency = new();
            currency.Amount = -10;
            currency.Type = CurrencyType.USD;

            Assert.That(() => account.Withdraw(currency), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void Should_ThrowEx_When_NotMoney()
        {
            Currency currency = new();
            currency.Amount = 9999;
            currency.Type = CurrencyType.USD;

            Assert.That(() => account.Withdraw(currency), Throws.TypeOf<ArgumentException>());
        }
    }
}
