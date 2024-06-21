using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItem _itemService;

        public ItemController(IItem itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("GetItems")]
        public async Task<IActionResult> GetItemsAsync([FromQuery] string? databaseName, [FromQuery] string? pattern)
        {
            var items = await _itemService.GetItemsAsync(databaseName, pattern);
            return Ok(items);
        }
    }
}
