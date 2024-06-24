using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using WebApplication2.Helpers;
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
            using (var connection = await DatabaseHelper.GetOpenConnectionAsync(_connectionStringProvider))
            using (var command = DatabaseHelper.CreateCommand(connection, "add_item_count",
                ("_cntcode", itemCount.ItemCountCode),
                ("_itmcode", itemCount.ItemCode),
                ("_itmdesc", itemCount.ItemDescription),
                ("_itmuom", itemCount.ItemUom),
                ("_itmbnl", itemCount.ItemBatchLotNumber),
                ("_itmexp", itemCount.ItemExpiry),
                ("_itmqty", itemCount.ItemQuantity)))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteItemCountAsync(string itemKey)
        {
            using (var connection = await DatabaseHelper.GetOpenConnectionAsync(_connectionStringProvider))
            using (var command = DatabaseHelper.CreateCommand(connection, "del_item_count", ("_itmkey", itemKey)))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task EditItemCountAsync(ItemCount itemCount)
        {
            using (var connection = await DatabaseHelper.GetOpenConnectionAsync(_connectionStringProvider))
            using (var command = DatabaseHelper.CreateCommand(connection, "edit_item_count",
                ("_itmkey", itemCount.ItemKey),
                ("_itmuom", itemCount.ItemUom),
                ("_itmbnl", itemCount.ItemBatchLotNumber),
                ("_itmexp", itemCount.ItemExpiry),
                ("_itmqty", itemCount.ItemQuantity)))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<IEnumerable<ItemCount>> ShowItemCountAsync(string countCode)
        {
            var itemCounts = new List<ItemCount>();
            using (var connection = await DatabaseHelper.GetOpenConnectionAsync(_connectionStringProvider))
            using (var command = DatabaseHelper.CreateCommand(connection, "show_item_count", ("_cntcode", countCode)))
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
            return itemCounts;
        }
    }
}
