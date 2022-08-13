using ApiTgBot.Models.EF;
using ApiTgBot.Models.EF.Tables;
using Microsoft.EntityFrameworkCore;

namespace ApiTgBot.Models.Repositories
{
    public class TransferRepository : IRepository<Transfer>
    {
        ApplicationContext db;
        public TransferRepository(ApplicationContext db)
        {
            this.db = db;
        }

        #region CRUD
        public IEnumerable<Transfer> GetAll() => db.Transfers;

        public Transfer Get(int id)
        {
            return (from a in db.Transfers
                    where a.Id == id
                    select a).FirstOrDefault(new Transfer());
        }

        public void Create(Transfer item) => db.Transfers.Add(item);

        public void Update(Transfer item) => db.Entry(item).State = EntityState.Modified;

        public void Delete(int id)
        {
            Transfer? transfer = db.Transfers.Find(id);
            if (transfer != null)
                db.Transfers.Remove(transfer);
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
