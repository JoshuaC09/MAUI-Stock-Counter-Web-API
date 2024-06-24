using WebApplication2.Interfaces;

namespace WebApplication2.Services
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private string _connectionString = string.Empty;
        private string _remoteDatabase = string.Empty;

        public async Task SetConnectionStringAsync(string connectionString, string remoteDatabase)
        {
            await Task.Yield();
            _connectionString = connectionString;
            _remoteDatabase = remoteDatabase;
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
    }
}
