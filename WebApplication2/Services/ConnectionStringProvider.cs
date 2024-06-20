using System.Threading.Tasks;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private string _connectionString = string.Empty;
        private string _remoteDatabase = string.Empty;
        private string _pattern = string.Empty;

        public async Task SetConnectionStringAsync(string connectionString, string remoteDatabase, string pattern)
        {
            await Task.Yield();
            _connectionString = connectionString;
            _remoteDatabase = remoteDatabase;
            _pattern = pattern;
        }

        public async Task<string> GetConnectionStringAsync()
        {
            await Task.Yield();
            return _connectionString;
        }

        public async Task<string> GetRemoteDatabaseAsync()
        {
            await Task.Yield();
            return _remoteDatabase;
        }

        public async Task<string> GetPatternAsync()
        {
            await Task.Yield();
            return _pattern;
        }
    }
}
