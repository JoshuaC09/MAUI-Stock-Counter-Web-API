using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurity _securityService;

        public TokenController(IConfiguration configuration, ISecurity securityService)
        {
            _configuration = configuration;
            _securityService = securityService;
        }

        [HttpPost("generate")]
        public IActionResult GenerateToken()
        {
            var key = _configuration["Jwt:Key"];
            var tokenString = _securityService.GenerateWebToken(key, "Stock_Counter");

            return Ok(new { Token = tokenString });
        }
    }
}
