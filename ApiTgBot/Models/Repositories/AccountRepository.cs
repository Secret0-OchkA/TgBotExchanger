using ApiTgBot.Models.EF;
using ApiTgBot.Models.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace ApiTgBot.Models.Repositories
{
    public class AccountRepository : IRepository<Account>
    {
        ApplicationContext db;
        public AccountRepository(ApplicationContext db)
        {
            this.db = db;
        }

        #region CRUD
        public IEnumerable<Account> GetAll() => db.Accounts;//TODO include

        public Account Get(int id)//TODO include
        {
            return (from a in db.Accounts
                    where a.Id == id
                    select a).FirstOrDefault(new Account());
        }

        public void Create(Account item) => db.Accounts.Add(item);

        public void Update(Account item) => db.Entry(item).State = EntityState.Modified;

        public void Delete(int id)
        {
            Account? account = db.Accounts.Find(id);
            if (account != null)
                db.Accounts.Remove(account);
        }

        public int Save() => db.SaveChanges();
        #endregion

        #region Dispose
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
