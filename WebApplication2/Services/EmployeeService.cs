﻿using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySqlConnector;
using WebApplication2.Helpers;
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
            string remoteDatabase = await _connectionStringProvider.GetRemoteDatabaseAsync() ?? databaseName;

            using (var connection = await DatabaseHelper.GetOpenConnectionAsync(_connectionStringProvider))
            using (var command = DatabaseHelper.CreateCommand(connection, "getemp", ("rmschma", remoteDatabase), ("patrn", pattern ?? string.Empty)))
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    employees.Add(new Employee
                    {
                        EmployeeId = reader.GetString("emp_id"),
                        EmployeeName = reader.GetString("emp_cname")
                    });
                }
            }
            return employees;
        }
    }
}
