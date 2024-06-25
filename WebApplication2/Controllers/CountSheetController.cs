using Microsoft.AspNetCore.Mvc;
using WebApplication2.Interfaces;
using WebApplication2.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  
    public class CountSheetController : ControllerBase
    {
        private readonly ICountSheet _countSheetService;


        public CountSheetController(ICountSheet countSheetService)
        {
            _countSheetService = countSheetService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddCountSheet([FromBody] CountSheet countSheet)
        {
            await _countSheetService.AddCountSheetAsync(countSheet.CountSheetEmployee, countSheet.CountDescription, countSheet.CountDate);
            return Ok();
        }

        [HttpDelete("delete/{countCode}")]
        public async Task<IActionResult> DeleteCountSheet(string countCode)
        {
            await _countSheetService.DeleteCountSheetAsync(countCode);
            return Ok();
        }

        [HttpPut("edit")]
        public async Task<IActionResult> EditCountSheet([FromBody] CountSheet countSheet)
        {
            await _countSheetService.EditCountSheetAsync(countSheet.CountCode, countSheet.CountDescription);
            return Ok();
        }

        [HttpGet("show/{emp}")]
        public async Task<ActionResult<IEnumerable<CountSheet>>> ShowCountSheet(string emp)
        {
            var countSheets = await _countSheetService.ShowCountSheetAsync(emp);
            return Ok(countSheets);
        }
    }
}
