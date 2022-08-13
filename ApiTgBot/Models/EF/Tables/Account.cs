namespace ApiTgBot.Models.EF.Tables
{
    public class Account : IAccount
    {
        public int Id { get; set; } = 0;
        public string? Name { get; set; } = null;
        public List<Currency> Wallet { get; set; } = new List<Currency>();

        public Currency Deposit(Currency money)
        {
            if (money.Amount < 0)
                throw new ArgumentException("Deposit money < 0");

            Currency? currency = (from c in Wallet
                                  where c.Type == money.Type
                                  select c).FirstOrDefault();
            if (currency != null)
            {
                currency += money;
                return currency;
            }

            Wallet.Add(money);
            return money;
        }

        public Currency Withdraw(Currency money)
        {
            if (money.Amount < 0)
                throw new ArgumentException("Withdraw money < 0");

            Currency? currency = (from c in Wallet
                                  where c.Type == money.Type
                                  select c).FirstOrDefault();

            if(currency == null)
                throw new ArgumentException("Withdraw not exists currency");

            if (currency.Amount < money.Amount)
                throw new ArgumentException("not enough money");

            currency -= money;
            return currency;
        }
    }

    public interface IAccount
    {
        Currency Deposit(Currency money);
        Currency Withdraw(Currency money);
    }
}
