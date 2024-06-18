using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Security;
using System.Net;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly DecryptionService _decryptionService;

        public DatabaseController(IConnectionStringProvider connectionStringProvider, DecryptionService decryptionService)
        {
            _connectionStringProvider = connectionStringProvider;
            _decryptionService = decryptionService;
        }

        [HttpPost("SetConnectionString")]
        public IActionResult SetConnectionString([FromBody] ConnString model)
        {
            if (string.IsNullOrEmpty(model.ConnectionString))
            {
                return BadRequest("Connection string is required.");
            }
            string decodedConnectionString = WebUtility.UrlDecode(model.ConnectionString);

            string decryptedConnectionString = _decryptionService.Decrypt(decodedConnectionString);
            _connectionStringProvider.SetConnectionString(decryptedConnectionString);
            return Ok("Connection string set successfully.");
        }

        [HttpGet("GetConnectionString")]
        public string GetConnectionString()
        {
            return _connectionStringProvider.GetConnectionString();
        }
    }
}
