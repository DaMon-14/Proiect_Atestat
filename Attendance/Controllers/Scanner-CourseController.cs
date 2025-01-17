using Microsoft.AspNetCore.Mvc;
using Attendance.Models;
using Attendance.Services;

namespace Attendance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Scanner_CourseController : ControllerBase
    {
        private readonly IScanner_Course _scanner_Course;

        public Scanner_CourseController(IScanner_Course scanner_Course)
        {
            _scanner_Course = scanner_Course;
        }

        [HttpGet]
        public async Task<ActionResult<List<Scanner_Course>>> GetScanner_Courses()
        {
            return Ok(await _scanner_Course.GetScanner_Courses());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Scanner_Course>> GetScanner_Course(uint id)
        {
            var scanner_course = await _scanner_Course.GetScanner_Course(id);
            if (scanner_course == null)
            {
                return NotFound();
            }

            return Ok(scanner_course);
        }

        [HttpPost]
        public async Task<ActionResult<Scanner_Course>> AddScanner_Course(UpdateScanner_Course scanner_course)
        {
            var newScanner_Course = await _scanner_Course.AddScanner_Course(scanner_course);
            return Ok(new
            {
                message = "Added Scanner_Course",
                Scanner_CourseId = newScanner_Course.Id
            });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Scanner_Course>> SetScanner_CourseStatus(uint id, bool isactive)
        {
            var scanner_course = await _scanner_Course.SetScanner_CourseStatus(id, isactive);
            if (scanner_course == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Updated Scanner_Course",
                Scanner_CourseId = scanner_course.isActive
            });
        }
    }
}
