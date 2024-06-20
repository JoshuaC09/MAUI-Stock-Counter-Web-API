using System.Threading.Tasks;

namespace WebApplication2.Interfaces
{
    public interface IConnectionStringProvider
    {
        Task SetConnectionStringAsync(string connectionString);
        Task<string> GetConnectionStringAsync();
    }
}