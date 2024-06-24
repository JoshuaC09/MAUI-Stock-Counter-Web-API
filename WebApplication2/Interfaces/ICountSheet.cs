using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface ICountSheet
    {
        Task AddCountSheetAsync(string emp, string desc, DateTime date);
        Task DeleteCountSheetAsync(string countCode);
        Task EditCountSheetAsync(string countCode, string desc);
        Task<IEnumerable<CountSheet>> ShowCountSheetAsync(string emp);
    }
}
