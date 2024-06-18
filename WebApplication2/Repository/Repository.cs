using Microsoft.EntityFrameworkCore;
using WebApplication1.DatabaseContext;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication2.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly MyDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(MyDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public T? GetById(int id) // Make the return type nullable
        {
            return _dbSet.Find(id); // _dbSet.Find(id) can return null
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
