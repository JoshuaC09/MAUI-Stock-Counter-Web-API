using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountSheetController : ControllerBase
    {
        private readonly ICountSheet _countSheetService;

        public CountSheetController(ICountSheet countSheetService)
        {
            _countSheetService = countSheetService;
        }

        // GET: api/CountSheet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountSheet>>> GetAll()
        {
            var countSheets = await _countSheetService.GetAllAsync().ConfigureAwait(false);
            return Ok(countSheets);
        }

        // GET: api/CountSheet/{countCode}
        [HttpGet("{countCode}")]
        public async Task<ActionResult<CountSheet>> GetById(string countCode)
        {
            var countSheet = await _countSheetService.GetByIdAsync(countCode).ConfigureAwait(false);
            if (countSheet == null)
            {
                return NotFound();
            }
            return Ok(countSheet);
        }

        // POST: api/CountSheet
        [HttpPost]
        public async Task<ActionResult<CountSheet>> Add([FromBody] CountSheet countSheet)
        {
            await _countSheetService.AddAsync(countSheet).ConfigureAwait(false);
            return CreatedAtAction(nameof(GetById), new { countCode = countSheet.CountCode }, countSheet);
        }

        // PUT: api/CountSheet/{countCode}
        [HttpPut("{countCode}")]
        public async Task<IActionResult> Update(string countCode, [FromBody] CountSheet countSheet)
        {
            if (countCode != countSheet.CountCode)
            {
                return BadRequest();
            }

            await _countSheetService.UpdateAsync(countSheet).ConfigureAwait(false);
            return NoContent();
        }

        // DELETE: api/CountSheet/{countCode}
        [HttpDelete("{countCode}")]
        public async Task<IActionResult> Delete(string countCode)
        {
            await _countSheetService.DeleteAsync(countCode).ConfigureAwait(false);
            return NoContent();
        }
    }
}
