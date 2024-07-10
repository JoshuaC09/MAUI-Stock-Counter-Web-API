using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication2.Interfaces;
using WebApplication2.Helpers;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IConnectionStringProvider _connectionStringProvider;
        private readonly ISecurity _securityService;
        private readonly ILogger<DatabaseController> _logger;

        public DatabaseController(IConnectionStringProvider connectionStringProvider, ISecurity securityService, ILogger<DatabaseController> logger)
        {
            _connectionStringProvider = connectionStringProvider;
            _securityService = securityService;
            _logger = logger;
        }

        [HttpPost("SetConnectionString")]
        public async Task<IActionResult> SetConnectionStringAsync([FromBody] ConnString model)
        {
            if (string.IsNullOrEmpty(model.ConnectionString))
            {
                return BadRequest("Connection string is required.");
            }

            string decodedConnectionString = WebUtility.UrlDecode(model.ConnectionString);
            _logger.LogInformation("Decoded connection string: {DecodedConnectionString}", decodedConnectionString);

            string decryptedConnectionString = decodedConnectionString;

            string coreConnectionString = string.Empty;
            string remoteDatabase = string.Empty;

            foreach (var part in decryptedConnectionString.Split(';'))
            {
                if (part.StartsWith("RemoteDatabase=", StringComparison.OrdinalIgnoreCase))
                {
                    remoteDatabase = part.Substring("RemoteDatabase=".Length);
                }
                else if (!part.StartsWith("PortNumber=", StringComparison.OrdinalIgnoreCase))
                {
                    if (!string.IsNullOrEmpty(coreConnectionString))
                    {
                        coreConnectionString += ";";
                    }
                    coreConnectionString += part;
                }
            }

            _logger.LogInformation("Core connection string: {CoreConnectionString}", coreConnectionString);
            _logger.LogInformation("Remote database: {RemoteDatabase}", remoteDatabase);

            bool connectionSuccessful = await DatabaseHelper.TestConnectionAsync(coreConnectionString);
            if (!connectionSuccessful)
            {
                return BadRequest("Unable to establish a connection to the database.");
            }

            await _connectionStringProvider.SetConnectionStringAsync(coreConnectionString, remoteDatabase);
            return Ok("Connection string set successfully.");
        }

        [Authorize]
        [HttpGet("GetConnectionString")]
        public async Task<string> GetConnectionStringAsync()
        {
            return await _connectionStringProvider.GetConnectionStringAsync();
        }

        [Authorize]
        [HttpGet("GetRemoteDatabase")]
        public async Task<string> GetRemoteDatabaseAsync()
        {
            return await _connectionStringProvider.GetRemoteDatabaseAsync();
        }
    }
}