using ApiTgBot.Models.EF;
using TgBot.Models.EF;
using TgBot.Models.EF.Tables;

namespace TgBot.Controllers
{
    public class AccountController
    {
        public IApplicationContext db;

        public AccountController(IApplicationContext db)
        {
            this.db = db;
        }

        public Account GetAccount(int id) 
        {
            throw new NotImplementedException();
        }
        public Account PutAccount(Account editAccount)
        {
            throw new NotImplementedException();
        }
        public Account PostAccount(Account newAccount)
        {
            throw new NotImplementedException();
        }
        public Account DeleteAccount(int id)
        {
            throw new NotImplementedException();
        }
    }
}
