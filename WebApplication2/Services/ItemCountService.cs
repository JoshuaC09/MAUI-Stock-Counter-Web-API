using System.Data;
using MySqlConnector;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class ItemCountService : IItemCount
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public ItemCountService(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public async Task AddItemCountAsync(ItemCount itemCount)
        {
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("add_item_count", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_cntcode", itemCount.ItemCountCode);
                    command.Parameters.AddWithValue("_itmcode", itemCount.ItemCode);
                    command.Parameters.AddWithValue("_itmdesc", itemCount.ItemDescription);
                    command.Parameters.AddWithValue("_itmuom", itemCount.ItemUom);
                    command.Parameters.AddWithValue("_itmbnl", itemCount.ItemBatchLotNumber);
                    command.Parameters.AddWithValue("_itmexp", itemCount.ItemExpiry);
                    command.Parameters.AddWithValue("_itmqty", itemCount.ItemQuantity);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteItemCountAsync(string itemKey)
        {
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("del_item_count", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_itmkey", itemKey);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EditItemCountAsync(ItemCount itemCount)
        {
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("edit_item_count", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_itmkey", itemCount.ItemKey);
                    command.Parameters.AddWithValue("_itmuom", itemCount.ItemUom);
                    command.Parameters.AddWithValue("_itmbnl", itemCount.ItemBatchLotNumber);
                    command.Parameters.AddWithValue("_itmexp", itemCount.ItemExpiry);
                    command.Parameters.AddWithValue("_itmqty", itemCount.ItemQuantity);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<ItemCount>> ShowItemCountAsync(string countCode)
        {
            List<ItemCount> itemCounts = new List<ItemCount>();
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("show_item_count", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_cntcode", countCode);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            itemCounts.Add(new ItemCount
                            {
                                ItemKey = reader.GetString("itm_key"),
                                ItemCounter = reader.GetInt32("itm_ctr"),
                                ItemCode = reader.GetString("itm_code"),
                                ItemDescription = reader.GetString("itm_description"),
                                ItemUom = reader.GetString("itm_uom"),
                                ItemBatchLotNumber = reader.GetString("itm_batchlot"),
                                ItemExpiry = reader.GetString("itm_expiry"),
                                ItemQuantity = reader.GetInt32("itm_quantity"),
                            });
                        }
                    }
                }
            }
            return itemCounts;
        }
    }
}
