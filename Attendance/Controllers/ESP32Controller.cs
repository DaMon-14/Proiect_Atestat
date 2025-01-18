using Attendance.Services;
using Microsoft.AspNetCore.Mvc;
using Attendance.Models;

namespace Attendance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ESP32Controller : ControllerBase
    {
        private readonly ICard _cards;
        private readonly IScanner_Course _scanner_courses;
        private readonly IEntry _entries;

        public ESP32Controller(ICard cardService, IScanner_Course scannercourseService, IEntry entryService)
        {
            _cards = cardService;
            _scanner_courses = scannercourseService;
            _entries = entryService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEntry([FromBody] ESP32 esp32)
        {
            var card = await _cards.GetCard(esp32.CardId);
            if (card == null) 
            {
                return NotFound(new { message = "Card not found" });    
            }
            if(!card.isActive) {
                return NotFound(new
                {
                    message = "Card not active"
                });
            }

            var scanner_course = await _scanner_courses.GetScanner_CourseByScannerId(esp32.ScannerId);
            if (scanner_course == null)
            {
                return NotFound(new { message = "Scanner not found" });
            }
            if(!scanner_course.isActive) {
                return NotFound(new
                {
                    message = "Scanner not active"
                });
            }

            var entry = await _entries.AddEntry(new UpdateEntry
            {
                ClientId = card.ClientId,
                CourseId = scanner_course.CourseId
            });
            if(entry == null) {
                return BadRequest(new
                {
                    message = "Failed to add entry"
                });
            }
            return Ok(new
            {
                message = "Added Entry",
                entry = entry
            });
        }
    }
}
