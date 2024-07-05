using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

            string decryptedConnectionString;
            try
            {
                decryptedConnectionString = await _securityService.DecryptAsync(decodedConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error decrypting connection string.");
                return BadRequest("Invalid connection string.");
            }

            _logger.LogInformation("Decrypted connection string: {DecryptedConnectionString}", decryptedConnectionString);

            string coreConnectionString = string.Empty;
            string remoteDatabase = string.Empty;

            foreach (var part in decryptedConnectionString.Split(';'))
            {
                if (part.StartsWith("RemoteDatabase=", StringComparison.OrdinalIgnoreCase))
                {
                    remoteDatabase = part.Substring("RemoteDatabase=".Length);
                }
                else
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

            await _connectionStringProvider.SetConnectionStringAsync(coreConnectionString, remoteDatabase);
            return Ok("Connection string set successfully.");
        }

        [HttpGet("GetConnectionString")]
        public async Task<string> GetConnectionStringAsync()
        {
            return await _connectionStringProvider.GetConnectionStringAsync();
        }

        [HttpGet("GetRemoteDatabase")]
        public async Task<string> GetRemoteDatabaseAsync()
        {
            return await _connectionStringProvider.GetRemoteDatabaseAsync();
        }
    }
}
