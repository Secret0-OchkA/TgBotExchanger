using ApiExchangeBot.Models.EF.Tables;


namespace ApiExchangeBot.Models
{
    public class AccountFacade
    {
        Account account;
        public AccountFacade(Account account)
        {
            this.account = account;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="money">how much currency should convert</param>
        /// <param name="exchangeRate">Convert param money in out type money</param>
        /// <returns></returns>
        public Account ConvertyMoney(Currency money, ExchangeRate exchangeRate)
        {
            account.Withdraw(money);
            Currency convertMoney = exchangeRate.Convert(money);
            account.Deposit(convertMoney);

            return account;
        }

        public Transfer Deposit(Currency money, string number, string code)
        {
            Transfer transfer = new Transfer
            {
                ownerTransfer = account,
                Amount = money,
                Number = number,
                Code = code,
            };

            return transfer;
        }

        public Account Withdraw(Currency money)
        {
            account.Withdraw(money);
            return account;
        }
    }
}
