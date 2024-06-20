using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ItemCountService : IItemCount
    {
        private readonly IRepository<ItemCount> _repository;

        public ItemCountService(IRepository<ItemCount> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ItemCount>> GetAllAsync()
        {
            return await _repository.GetAllAsync().ConfigureAwait(false);
        }

        public async Task<ItemCount?> GetByIdAsync(string itemCounter)
        {
            if (int.TryParse(itemCounter, out int counter))
            {
                return await _repository.GetByIdAsync(counter).ConfigureAwait(false);
            }
            return null;
        }

        public async Task AddAsync(ItemCount itemCount)
        {
            await _repository.AddAsync(itemCount).ConfigureAwait(false);
            await _repository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateAsync(ItemCount itemCount)
        {
            await _repository.UpdateAsync(itemCount);
            await _repository.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task DeleteAsync(string itemCounter)
        {
            var itemCount = await GetByIdAsync(itemCounter).ConfigureAwait(false);
            if (itemCount != null)
            {
                await _repository.DeleteAsync(itemCount).ConfigureAwait(false);
                await _repository.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
