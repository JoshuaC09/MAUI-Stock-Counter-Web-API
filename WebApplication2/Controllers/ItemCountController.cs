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

        // GET: api/ItemCount/{itemCounter}
        [HttpGet("{itemCounter}")]
        public async Task<ActionResult<ItemCount>> GetById(string itemCounter)
        {
            var itemCount = await _itemCountService.GetByIdAsync(itemCounter).ConfigureAwait(false);
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
            return CreatedAtAction(nameof(GetById), new { itemCounter = itemCount.ItemCounter.ToString() }, itemCount);
        }

        // PUT: api/ItemCount/{itemCounter}
        [HttpPut("{itemCounter}")]
        public async Task<IActionResult> Update(string itemCounter, [FromBody] ItemCount itemCount)
        {
            if (itemCounter != itemCount.ItemCounter.ToString())
            {
                return BadRequest();
            }

            await _itemCountService.UpdateAsync(itemCount).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/ItemCount/{itemCounter}
        [HttpDelete("{itemCounter}")]
        public async Task<IActionResult> Delete(string itemCounter)
        {
            await _itemCountService.DeleteAsync(itemCounter).ConfigureAwait(false);
            return NoContent();
        }
    }
}
