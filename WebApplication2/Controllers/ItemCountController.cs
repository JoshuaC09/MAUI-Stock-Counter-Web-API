using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        // GET: api/ItemCount
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCount>>> GetAll()
        {
            var itemCounts = await _itemCountService.GetAllAsync().ConfigureAwait(false);
            return Ok(itemCounts);
        }

        // GET: api/ItemCount/{itemKey}
        [HttpGet("{itemKey}")]
        public async Task<ActionResult<ItemCount>> GetById(string itemKey)
        {
            var itemCount = await _itemCountService.GetByIdAsync(itemKey).ConfigureAwait(false);
            if (itemCount == null)
            {
                return NotFound();
            }
            return Ok(itemCount);
        }

        // POST: api/ItemCount
        [HttpPost]
        public async Task<ActionResult<ItemCount>> Add([FromBody] ItemCount itemCount)
        {
            await _itemCountService.AddAsync(itemCount).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetById), new { itemKey = itemCount.ItemKey }, itemCount);
        }

        // PUT: api/ItemCount/{itemKey}
        [HttpPut("{itemKey}")]
        public async Task<IActionResult> Update(string itemKey, [FromBody] ItemCount itemCount)
        {
            if (itemKey != itemCount.ItemKey)
            {
                return BadRequest();
            }

            await _itemCountService.UpdateAsync(itemCount).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/ItemCount/{itemKey}
        [HttpDelete("{itemKey}")]
        public async Task<IActionResult> Delete(string itemKey)
        {
            await _itemCountService.DeleteAsync(itemKey).ConfigureAwait(false);
            return NoContent();
        }
    }
}
