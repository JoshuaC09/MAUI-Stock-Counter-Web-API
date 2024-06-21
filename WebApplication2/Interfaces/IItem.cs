using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IItem
    {
        Task<IEnumerable<Item>> GetItemsAsync(string databaseName, string? pattern);
    }
}
