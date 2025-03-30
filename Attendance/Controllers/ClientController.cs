using AttendanceAPI.Interfaces;
using AttendanceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AttendanceAPI.EF.DBO;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IUser _users;
        private readonly IAttendance _entries;
        private readonly ICourse _courses;
        private readonly IClient_Course _clientCourses;
        private readonly IConfiguration _configuration;
        public ClientController(IUser userService, IAttendance entryService, IConfiguration configuration, ICourse courses, IClient_Course clientCourses)
        {
            _users = userService;
            _entries = entryService;
            _configuration = configuration;
            _courses = courses;
            _clientCourses = clientCourses;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetClientAttendances(uint id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var attendances = await _entries.GetEntries();
            attendances = attendances.Where(x => x.ClientId == id).ToList();
            if (attendances.Count()==0)
            {
                return NotFound();
            }
            return Ok(attendances);
        }

        [HttpGet]
        [Route("client/{id}")]
        public async Task<IActionResult> GetUser(uint id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var client = await _users.GetUser(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpGet]
        [Route("course/{id}")]
        public async Task<IActionResult> GetCourses(uint id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var activeCourses = await _courses.GetActiveCourses();
            var course = activeCourses.FirstOrDefault(x => x.CourseId == id);
            
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpGet]
        [Route("courses/{id}")]
        public async Task<IActionResult> GetCourse(uint id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var allCourses = await _clientCourses.GetAll();
            var ClientCourses = allCourses.Where(x => x.ClientId == id).ToList();
            var activeCourses = await _courses.GetActiveCourses();
            var courses = new List<CourseDBO>();
            courses = activeCourses.Where(x => ClientCourses.Any(y => y.CourseId == x.CourseId)).ToList();
            if (courses == null || courses.Count()==0)
            {
                return NotFound();
            }
            return Ok(courses);
        }
    }
}
