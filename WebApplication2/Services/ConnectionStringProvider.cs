using System.Threading.Tasks;
using WebApplication2.Interfaces;

namespace WebApplication2.Services
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private string _connectionString = string.Empty;

        public async Task SetConnectionStringAsync(string connectionString)
        {
            // Simulate async work
            await Task.Yield();
            _connectionString = connectionString;
        }

        public async Task<string> GetConnectionStringAsync()
        {
            // Simulate async work
            await Task.Yield();
            return _connectionString;
        }
    }
}