using Microsoft.AspNetCore.Mvc;
using Attendance.Services;
using Attendance.Models;

namespace Attendance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientCourseController : ControllerBase
    {
        private readonly IClient_Course _client_Courses;
        public ClientCourseController(IClient_Course courseService)
        {
            _client_Courses = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            return Ok(await _client_Courses.GetClient_Courses());
        }

        [HttpGet]
        [Route("get/{courseid}")]
        public async Task<IActionResult> GetCourse(uint courseid)
        {
            var course = await _client_Courses.GetClient_Course(courseid);
            if(course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateCourse(uint id, [FromBody] UpdateClient_Course course)
        {
            if(course == null)
            {
                return BadRequest();
            }

            var updatedCourse = await _client_Courses.UpdateClient_Course(id, course);
            
            return Ok(new
            {
                message = "Updated Course",
                CourseId = updatedCourse.CourseId
            });
        }

        [HttpPost]
        [Route("add/{courseid}/{clientid}")]
        public async Task<IActionResult> AddCourse(int clientid, int courseid, [FromBody] UpdateClient_Course course)
        {
            if(course == null)
            {
                return BadRequest();
            }

            //var updatedCourse = await _courses.UpdateClient_Course(courseid, course);
            var updatedCourse = await _client_Courses.AddClient_Course(clientid, courseid, course);
            
            return Ok(new
            {
                message = "Added Course",
                CourseId = updatedCourse.CourseId
            });
        }
    }
}
