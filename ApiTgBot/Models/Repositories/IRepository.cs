namespace ApiTgBot.Models.Repositories
{
    public interface IRepository<T> : IDisposable 
        where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        int Save();
    }
}
