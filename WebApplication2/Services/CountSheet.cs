using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Repository;

namespace WebApplication2.Services
{
    public class CountSheetService : ICountSheetService
    {
        private readonly IRepository<CountSheet> _repository;

        public CountSheetService(IRepository<CountSheet> repository)
        {
            _repository = repository;
        }

        public IEnumerable<CountSheet> GetAll()
        {
            return _repository.GetAll();
        }

        public CountSheet? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Add(CountSheet countSheet)
        {
            _repository.Add(countSheet);
            _repository.SaveChanges();
        }

        public void Update(CountSheet countSheet)
        {
            _repository.Update(countSheet);
            _repository.SaveChanges();
        }

        public void Delete(int id)
        {
            var countSheet = _repository.GetById(id);
            if (countSheet != null)
            {
                _repository.Delete(countSheet);
                _repository.SaveChanges();
            }
        }

        public void SaveChanges()
        {
            _repository.SaveChanges();
        }
    }
}
