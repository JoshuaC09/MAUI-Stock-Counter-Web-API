using System.Threading.Tasks;
using WebApplication2.Interfaces;

namespace WebApplication2.Services
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private string _connectionString = string.Empty;
        private string _remoteDatabase = string.Empty;

        public Task SetConnectionStringAsync(string connectionString, string remoteDatabase)
        {
            _connectionString = connectionString;
            _remoteDatabase = remoteDatabase;
            return Task.CompletedTask;
        }

        public Task<string> GetConnectionStringAsync()
        {
            return Task.FromResult(_connectionString);
        }

        public Task<string> GetRemoteDatabaseAsync()
        {
            return Task.FromResult(_remoteDatabase);
        }
    }
}
