using Microsoft.AspNetCore.Mvc;
using Attendance.Services;
using Attendance.Models;

namespace Attendance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScannerController : ControllerBase
    {
        private readonly IScanner _scanner;
        public ScannerController(IScanner scannerService)
        {
            _scanner = scannerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetScanners()
        {
            return Ok(await _scanner.GetScanners());
        }

        [HttpGet]
        [Route("get/{scannerid}")]
        public async Task<IActionResult> GetScanner(uint scannerid)
        {
            var scanner = await _scanner.GetScanner(scannerid);
            if(scanner == null)
            {
                return NotFound();
            }
            return Ok(scanner);
        }

        [HttpPost]
        public async Task<IActionResult> AddScanner([FromBody] UpdateScanner scanner)
        {
            if(scanner == null)
            {
                return BadRequest();
            }

            var newScanner = await _scanner.AddScanner(scanner);
            
            return Ok(new
            {
                message = "Added Scanner",
                ScannerId = newScanner.ScannerId
            });
        }

        [HttpPut]
        [Route("update/{scannerid}")]
        public async Task<IActionResult> UpdateScanner(uint scannerid, [FromBody] UpdateScanner scanner)
        {
            var updatedScanner = await _scanner.UpdateScanner(scannerid, scanner);
            if(updatedScanner == null)
            {
                return NotFound();
            }
            return Ok(updatedScanner);
        }

        [HttpPut]
        [Route("setstatus/{scannerid}")]
        public async Task<IActionResult> SetScannerStatus(uint scannerid, [FromBody] bool isactive)
        {
            var updatedScanner = await _scanner.SetScannerStatus(scannerid, isactive);
            if(updatedScanner == null)
            {
                return NotFound();
            }
            return Ok(updatedScanner);
        }
    }
}
