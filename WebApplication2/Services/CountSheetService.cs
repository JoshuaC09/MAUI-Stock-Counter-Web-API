    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebApplication2.Interfaces;
    using WebApplication2.Models;
    using WebApplication2.Repository;

    namespace WebApplication2.Services
    {
        public class CountSheetService : ICountSheet
        {
            private readonly IRepository<CountSheet> _repository;

            public CountSheetService(IRepository<CountSheet> repository)
            {
                _repository = repository;
            }

            public async Task<IEnumerable<CountSheet>> GetAllAsync()
            {
                return await _repository.GetAllAsync();
            }

            public async Task<CountSheet?> GetByIdAsync(string countCode)
            {
                return await _repository.GetByIdAsync(countCode);
            }

            public async Task AddAsync(CountSheet countSheet)
            {
                await _repository.AddAsync(countSheet);
                await _repository.SaveChangesAsync();
            }

            public async Task UpdateAsync(CountSheet countSheet)
            {
                await _repository.UpdateAsync(countSheet);
                await _repository.SaveChangesAsync();
            }

            public async Task DeleteAsync(string countCode)
            {
                var countSheet = await _repository.GetByIdAsync(countCode);
                if (countSheet != null)
                {
                    await _repository.DeleteAsync(countSheet);
                    await _repository.SaveChangesAsync();
                }
            }
        }
    }
