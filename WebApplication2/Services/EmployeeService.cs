using System.Data;
using MySqlConnector;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class EmployeeService : IEmployee
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public EmployeeService(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(string databaseName, string pattern)
        {
            var employees = new List<Employee>();
            string connectionString = await _connectionStringProvider.GetConnectionStringAsync();

            using (var connection = new MySqlConnection(connectionString))
            {
                await connection.OpenAsync();
                using (var command = new MySqlCommand("getemp", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("rmschma", databaseName);
                    command.Parameters.AddWithValue("patrn", pattern ?? string.Empty);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var employee = new Employee
                            {
                                EmployeeId = reader.GetString("emp_id"),
                                EmployeeName = reader.GetString("emp_cname")
                            };
                            employees.Add(employee);
                        }
                    }
                }
            }

            return employees;
        }
    }
}
