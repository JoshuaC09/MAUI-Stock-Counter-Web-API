using WebApplication2.Interfaces;
using WebApplication2.Models;
using MySqlConnector;
using System.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication2.Services
{
    public class CountSheetService : ICountSheet
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public CountSheetService(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public async Task AddCountSheetAsync(string emp, string desc, DateTime date)
        {
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("add_count_sheet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_emp", emp);
                    command.Parameters.AddWithValue("_desc", desc);
                    command.Parameters.AddWithValue("_date", date);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteCountSheetAsync(string countCode)
        {
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("del_count_sheet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_cntcode", countCode);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task EditCountSheetAsync(string countCode, string desc)
        {
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("edit_count_sheet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_cntcode", countCode);
                    command.Parameters.AddWithValue("_desc", desc);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<CountSheet>> ShowCountSheetAsync(string emp)
        {
            List<CountSheet> countSheets = new List<CountSheet>();
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();
            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("show_count_sheet", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("_emp", emp);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            countSheets.Add(new CountSheet
                            {
                                CountCode = reader.GetString("cnt_code"),
                                CountDescription = reader.GetString("cnt_desc"),
                                CountSheetEmployee = emp
                            });
                        }
                    }
                }
            }
            return countSheets;
        }
    }
}
