using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface ICountSheetService
    {
        IEnumerable<CountSheet> GetAll();
        CountSheet? GetById(int id);
        void Add(CountSheet countSheet);
        void Update(CountSheet countSheet);
        void Delete(int id);
        void SaveChanges();
    }
}
