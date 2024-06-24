namespace WebApplication2.Interfaces
{
    public interface IConnectionStringProvider
    {
        Task SetConnectionStringAsync(string connectionString, string remoteDatabase);
        Task<string> GetConnectionStringAsync();
        Task<string> GetRemoteDatabaseAsync();
    }
}
