using ApiTgBot.Models.EF;
using ApiTgBot.Models.EF.Tables;
using ApiTgBot.Models.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace ApiTgBot.Models
{
    public class AccountProxy
    {
        Account account;
        public AccountProxy(Account account)
        {
            this.account = account;
        }

        public int ConvertyMoney(int AccountId, Currency money, CurrencyType outType)
        {
            throw new NotImplementedException();
            //if (exchangeRate != null)
            //{
            //    account.Withdraw(money);
            //    Currency convertMoney = exchangeRate.Convert(money);
            //    account.Deposit(convertMoney);
            //}
            //return db.SaveChanges();
        }
        public void Deposit(Currency money, string number, string code)
        {
            Transfer transfer = new Transfer
            {
                ownerTransfer = account,
                Amount = money,
                Number = number,
                Code = code,
            };
        }
    }
}
