using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface ICountSheet
    {
        Task<IEnumerable<CountSheet>> GetAllAsync();
        Task<CountSheet?> GetByIdAsync(string countCode);
        Task AddAsync(CountSheet countSheet);
        Task UpdateAsync(CountSheet countSheet);
        Task DeleteAsync(string countCode);
    }
}
