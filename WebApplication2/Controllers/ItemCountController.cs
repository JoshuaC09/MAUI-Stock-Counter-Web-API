using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCountController : ControllerBase
    {
        private readonly IItemCount _itemCountService;

        public ItemCountController(IItemCount itemCountService)
        {
            _itemCountService = itemCountService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddItemCount([FromBody] ItemCount itemCount)
        {
            await _itemCountService.AddItemCountAsync(itemCount);
            return Ok();
        }

        [HttpDelete("delete/{itemKey}")]
        public async Task<IActionResult> DeleteItemCount(string itemKey)
        {
            await _itemCountService.DeleteItemCountAsync(itemKey);
            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditItemCount([FromBody] ItemCount itemCount)
        {
            await _itemCountService.EditItemCountAsync(itemCount);
            return Ok();
        }

        [HttpGet("show/{countCode}")]
        public async Task<IEnumerable<ItemCount>> ShowItemCount(string countCode)
        {
            return await _itemCountService.ShowItemCountAsync(countCode);
        }
    }
}
