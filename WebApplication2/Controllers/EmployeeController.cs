using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _employeeService;

        public EmployeeController(IEmployee employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees([FromQuery] string databaseName, [FromQuery] string pattern = null)
        {
            if (string.IsNullOrEmpty(databaseName))
            {
                return BadRequest("Database name is required.");
            }

            var employees = await _employeeService.GetEmployeesAsync(databaseName, pattern);
            return Ok(employees);
        }
    }
}
