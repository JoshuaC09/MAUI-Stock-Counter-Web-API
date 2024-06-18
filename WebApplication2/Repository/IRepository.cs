namespace WebApplication2.Repository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T? GetById(int id);
        IEnumerable<T> GetAll();
        void SaveChanges();
    }
}
