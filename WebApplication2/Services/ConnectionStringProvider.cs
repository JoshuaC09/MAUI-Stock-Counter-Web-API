using WebApplication2.Interfaces;

namespace WebApplication2.Services
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private string _connectionString = string.Empty;

        public void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
