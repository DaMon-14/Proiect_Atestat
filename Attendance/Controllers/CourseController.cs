using Attendance.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Attendance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourse _courses;
        public CourseController(ICourse courseService)
        {
            _courses = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCourses()
        {
            return Ok(await _courses.GetCourses());
        }

        [HttpGet]
        [Route("get/{courseid}")]
        public async Task<IActionResult> GetCourses(uint courseid)
        {
            var course = await _courses.GetCourse(courseid);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }
    }
}
