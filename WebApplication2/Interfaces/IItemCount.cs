using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IItemCount
    {
        Task<IEnumerable<ItemCount>> GetAllAsync();
        Task<ItemCount?> GetByIdAsync(string itemCounter);
        Task AddAsync(ItemCount itemCount);
        Task UpdateAsync(ItemCount itemCount);
        Task DeleteAsync(string itemCounter);
    }
}
