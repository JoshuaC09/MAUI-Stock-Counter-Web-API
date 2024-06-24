using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IItemCount
    {
        Task AddItemCountAsync(ItemCount itemCount);
        Task DeleteItemCountAsync(string itemKey);
        Task EditItemCountAsync(ItemCount itemCount);
        Task<IEnumerable<ItemCount>> ShowItemCountAsync(string countCode);
    }
}
