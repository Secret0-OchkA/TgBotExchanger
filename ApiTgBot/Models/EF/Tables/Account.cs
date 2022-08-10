namespace ApiTgBot.Models.EF.Tables
{
    public partial class Account
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Currency> Wallet { get; set; }

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

            if (currency != null)
            {
                currency -= money;
                return currency;
            }

            throw new ArgumentException("Withdraw not exists currency");
        }
    }
}
