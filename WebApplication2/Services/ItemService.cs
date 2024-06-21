using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ItemService : IItem
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public ItemService(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string databaseName, string? pattern)
        {
            var items = new List<Item>();
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
            string remoteDatabase = await _connectionStringProvider.GetRemoteDatabaseAsync() ?? databaseName;

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("getinv", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("rmschma", remoteDatabase);
                    command.Parameters.AddWithValue("patrn", pattern ?? string.Empty);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var item = new Item
                            {
                                ItemNumber = reader.GetString("itemno"),
                                ItemDescription = reader.GetString("itemdesc"),
                                SellingUom = reader.GetString("suom")
                            };
                            items.Add(item);
                        }
                    }
                }
            }

            return items;
        }
    }
}
