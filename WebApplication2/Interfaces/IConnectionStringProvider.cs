namespace WebApplication2.Interfaces
{
    public interface IConnectionStringProvider
    {
        void SetConnectionString(string connectionString);
        string GetConnectionString();
    }
}
