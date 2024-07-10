using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IInventory
    {
        Task InitInventoryAsync();
        Task PostInventoryAsync();
        Task<List<ExportedItem>> ExportInventoryAsync();
    }
}
