using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountSheetController : ControllerBase
    {
        private readonly ICountSheetService _countSheetService;

        public CountSheetController(ICountSheetService countSheetService)
        {
            _countSheetService = countSheetService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CountSheet>> GetAll()
        {
            return Ok(_countSheetService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<CountSheet> GetById(int id)
        {
            var countSheet = _countSheetService.GetById(id);
            if (countSheet == null)
            {
                return NotFound();
            }
            return Ok(countSheet);
        }

        [HttpPost]
        public ActionResult Add(CountSheet countSheet)
        {
            _countSheetService.Add(countSheet);
            return CreatedAtAction(nameof(GetById), new { id = countSheet.CountId }, countSheet);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, CountSheet countSheet)
        {
            if (id != countSheet.CountId)
            {
                return BadRequest();
            }

            _countSheetService.Update(countSheet);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _countSheetService.Delete(id);
            return NoContent();
        }
    }
}
