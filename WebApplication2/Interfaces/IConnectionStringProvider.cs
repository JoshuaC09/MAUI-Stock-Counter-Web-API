using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.Interfaces
{
    public interface IConnectionStringProvider
    {
        Task SetConnectionStringAsync(string connectionString, string remoteDatabase, string pattern);
        Task<string> GetConnectionStringAsync();
        Task<string> GetRemoteDatabaseAsync();
        Task<string> GetPatternAsync();
    }
}
