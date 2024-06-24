using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using WebApplication2.Security;

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
        public async Task<IActionResult> SetConnectionStringAsync([FromBody] ConnString model)
        {
            if (string.IsNullOrEmpty(model.ConnectionString))
            {
                return BadRequest("Connection string is required.");
            }

            string decodedConnectionString = WebUtility.UrlDecode(model.ConnectionString);
            string decryptedConnectionString = await _decryptionService.DecryptAsync(decodedConnectionString);

            string coreConnectionString = string.Empty;
            string remoteDatabase = string.Empty;

            foreach (var part in decryptedConnectionString.Split(';'))
            {
                if (part.StartsWith("RemoteDatabase="))
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
