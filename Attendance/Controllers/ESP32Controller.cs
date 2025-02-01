using Microsoft.AspNetCore.Mvc;
using AttendanceAPI.Models;
using AttendanceAPI.Interfaces;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ESP32Controller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICard _cards;
        private readonly IScanner_Course _scanner_courses;
        private readonly IAttendance _entries;
        private readonly IClient_Course _client_courses;

        public ESP32Controller(IConfiguration config, ICard cardService, IScanner_Course scannercourseService, IAttendance entryService, IClient_Course client_courseService)
        {
            _configuration = config;
            _cards = cardService;
            _scanner_courses = scannercourseService;
            _entries = entryService;
            _client_courses = client_courseService;
            
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddEntry([FromBody] ESP32 esp32, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
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
            /*    Not needed for now
             
            var client_course = await _client_courses.GetClient_courseByClientIdAndCourseId(card.ClientId, scanner_course.CourseId);
            if(client_course == null || !client_course.isEnrolled) {
                return NotFound(new
                {
                    message = "Client not enrolled"
                });
            }
            if (client_course.Points == 0)
            {
                return NotFound(new
                {
                    message = "Not Enough Points"
                });
            }
            */
            var entry = await _entries.AddEntry(new Attendance
            {
                ClientId = card.ClientId,
                CourseId = scanner_course.CourseId
            });
            if (entry == null)
            {
                return BadRequest(new
                {
                    message = "Failed to add entry"
                });
            }

            return Ok();
        }
    }
}
