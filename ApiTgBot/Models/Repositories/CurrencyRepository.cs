using ApiTgBot.Models.EF;
using ApiTgBot.Models.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace ApiTgBot.Models.Repositories
{
    public class CurrencyRepository : IRepository<Currency>
    {
        ApplicationContext db;
        public CurrencyRepository(ApplicationContext db)
        {
            this.db = db;
        }

        #region CRUD
        public IEnumerable<Currency> GetAll() => db.Currencies;//TODO include

        public Currency Get(int id)
        { 
            return (from a in db.Currencies
                    where a.Id == id
                    select a).FirstOrDefault(new Currency());
        }//TODO include

        public void Create(Currency item) => db.Currencies.Add(item);

        public void Update(Currency item) => db.Entry(item).State = EntityState.Modified;

        public void Delete(int id)
        {
            Currency? Currency = db.Currencies.Find(id);
            if (Currency != null)
                db.Currencies.Remove(Currency);
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
