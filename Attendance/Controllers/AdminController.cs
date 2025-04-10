using AttendanceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using System.Globalization;
using System.Net;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICard _cards;
        private readonly IUser _users;
        private readonly IAttendance _entries;
        private readonly ICourse _courses;
        private readonly IScanner _scanners;
        private readonly IScanner_Course _scanner_courses;
        private readonly IConfiguration _configuration;
        private readonly IClient_Course _client_courses;
        public AdminController(IUser userService, IAttendance entryService, IConfiguration config, ICourse courses, IScanner scanners, IScanner_Course scanner_courses, ICard cards, IClient_Course client_courses)
        {
            _users = userService;
            _entries = entryService;
            _configuration = config;
            _courses = courses;
            _scanners = scanners;
            _scanner_courses = scanner_courses;
            _cards = cards;
            _client_courses = client_courses;
        }
        [HttpGet]
        [Route("clients")]
        public async Task<IActionResult> GetAllUsers([FromHeader] string UID)
        {
            if(UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var clients = await _users.GetAllUsers();
            if(clients.Count() == 0)
            {
                return NotFound();
            }
            return Ok(clients);
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
            if(client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        [Route("client")]
        public async Task<IActionResult> AddUser([FromBody] UpdateUser client, [FromHeader] string UID)
        {
            if (client == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var users = await _users.GetAllUsers();
            if (client.UserName == null)
            {
                return BadRequest("Username is mandatory");
            }
            else
            {
                if (users.FirstOrDefault(x => x.UserName == client.UserName) != null)
                {
                    return new ObjectResult("User already exists")
                    {
                        StatusCode = (int)HttpStatusCode.Conflict
                    };
                }
            }
            if(client.Password != null)
            {
                if (client.Password.Length < 8)
                {
                    return BadRequest("Password must be longer than 8 caracters");
                }
            }
            else
            {
                return BadRequest("Password is mandatory");
            }
            

                var newclient = await _users.AddUser(client);
            if(newclient == null)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpPut]
        [Route("client")]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateUser clientinfo, [FromHeader] string UID)
        {
            if(clientinfo == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            if (clientinfo.Password.Length < 8 && clientinfo.Password!="")
            {
                return BadRequest("Password must be longer than 8 caracters");
            }
            var client = await _users.UpdateUser(clientinfo);
            if (client == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet]
        [Route("entries")]
        public async Task<IActionResult> GetAllEntries([FromHeader] string UID)
        {
            if(UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var entries = await _entries.GetEntries();   
            if(entries.Count()==0)
            {
                return NotFound();
            }
            return Ok(entries);
        }

        [HttpGet]
        [Route("entryById/{Id}")]
        public async Task<IActionResult> GetEntryById(uint Id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var entries = await _entries.GetEntryById(Id);
            if (entries == null)
            {
                return NotFound();
            }
            return Ok(entries);
        }

        [HttpGet]
        [Route("entryByClient/{clientId}")]
        public async Task<IActionResult> GetEntriesByClientId(uint clientId, [FromHeader] string UID)
        {
            if(UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var entries = await _entries.GetEntries();
            entries = entries.Where(x => x.ClientId == clientId).ToList();
            if(entries.Count()==0)
            {
                return NotFound();
            }
            return Ok(entries);
        }

        [HttpGet]
        [Route("entryByCourse/{courseId}")]
        public async Task<IActionResult> GetEntriesByCourseId(uint courseId, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var entries = await _entries.GetEntries();
            entries = entries.Where(x => x.CourseId == courseId).ToList();
            if(entries.Count() == 0)
            {
                return NotFound();
            }
            return Ok(entries);
        }

        [HttpPost]
        [Route("addEntry")]
        public async Task<IActionResult> AddEntry([FromBody] Attendance entry, [FromHeader] string UID)
        {
            if(entry == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }

            var newclient = await _entries.AddEntry(entry);
            if(newclient == "Ok")
            {
                return Ok();
            }
            return NotFound(newclient);
        }

        [HttpPut]
        [Route("updateEntry")]
        public async Task<IActionResult> UpdateEntry([FromBody] AttendanceDBO entry, [FromHeader] string UID)
        {
            if(entry == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }

            var updateEntry = await _entries.UpdateEntry(entry);
            if (updateEntry == "Ok")
            {
                return Ok();
            }
            return NotFound(updateEntry);
        }

        [HttpDelete]
        [Route("entry/{id}")]
        public async Task<IActionResult> DeleteEntry(int id, [FromHeader] string UID)
        {
            if(UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var entry = await _entries.DeleteEntry(id);
            if (entry == false)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Deleted Entry"
            });
        }

        [HttpGet]
        [Route("courses")]
        public async Task<IActionResult> GetActiveCourses([FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var courses = await _courses.GetActiveCourses();
            if (courses.Count() == 0)
            {
                return NotFound();
            }
            return Ok(courses);
        }

        [HttpGet]
        [Route("allcourses")]
        public async Task<IActionResult> GetAllCourses([FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var courses = await _courses.GetAllCourses();
            if (courses.Count() == 0)
            {
                return NotFound();
            }
            return Ok(courses);
        }

        [HttpGet]
        [Route("course/{id}")]
        public async Task<IActionResult> GetCourse(uint id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var course = await _courses.GetCourse(id);
            if (course == null)
            {
                return NotFound();
            }
            return Ok(course);
        }

        [HttpPost]
        [Route("addCourse")]
        public async Task<IActionResult> AddCourse([FromBody] Course course, [FromHeader] string UID)
        {
            if (course == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var newclient = await _courses.AddCourse(course);
            if (newclient == null)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpPut]
        [Route("updateCourse")]
        public async Task<IActionResult> UpdateCourse([FromBody] CourseDBO course, [FromHeader] string UID)
        {
            if (course == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var client = await _courses.UpdateCourse(course);
            if (client == null)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("deleteCourse/{courseId}")]
        public async Task<IActionResult> DeleteCourse(uint courseId, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var client = await _courses.DeleteCourse(courseId);
            if (client == false)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpGet]
        [Route("scanners")]
        public async Task<IActionResult> GetAllScanners([FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var scanner = await _scanners.GetAllScanners();
            if (scanner.Count() == 0)
            {
                return NotFound();
            }
            return Ok(scanner);
        }

        [HttpGet]
        [Route("scanner/{id}")]
        public async Task<IActionResult> GetScanner(uint id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var scanner = await _scanners.GetScanner(id);
            if (scanner == null)
            {
                return NotFound();
            }
            return Ok(scanner);
        }

        [HttpPost]
        [Route("addScanner")]
        public async Task<IActionResult> AddScanner([FromBody] Scanner scanner, [FromHeader] string UID)
        {
            if (scanner == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var newclient = await _scanners.AddScanner(scanner);
            if (newclient == null)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpPut]
        [Route("updateScanner")]
        public async Task<IActionResult> UpdateScanner([FromBody] ScannerDBO scanner, [FromHeader] string UID)
        {
            if (scanner == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var Scanner = await _scanners.UpdateScanner(scanner);
            if (Scanner == null)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpDelete]
        [Route("deleteScanner/{scannerId}")]
        public async Task<IActionResult> DeleteScanner(uint scannerId, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var scanner = await _scanners.DeleteScanner(scannerId);
            if (scanner == false)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        [HttpGet]
        [Route("scanner_courses")]
        public async Task<IActionResult> GetAllScanner_Courses([FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var scanner_courses = await _scanner_courses.GetScanner_Courses();
            if (scanner_courses.Count() == 0)
            {
                return NotFound();
            }
            return Ok(scanner_courses);
        }

        [HttpGet]
        [Route("scanner_course/{id}")]
        public async Task<IActionResult> GetScanner_Course(uint id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var scanner_courses = await _scanner_courses.GetScanner_Courses();
            var scanner_course = scanner_courses.FirstOrDefault(x => x.Id == id);
            if (scanner_course == null)
            {
                return NotFound();
            }
            return Ok(scanner_course);
        }

        [HttpPost]
        [Route("addScanner_Course")]
        public async Task<IActionResult> AddScanner_Course([FromBody] Scanner_Course scanner, [FromHeader] string UID)
        {
            if (scanner == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var newscanner_course = await _scanner_courses.AddScanner_Course(scanner);
            if (newscanner_course == "Ok")
            {
                return Ok();
            }
            return NotFound(newscanner_course);
            
        }

        [HttpPut]
        [Route("updateScanner_Course")]
        public async Task<IActionResult> UpdateScanner_Course([FromBody] Scanner_CourseDBO scanner_courseinfo, [FromHeader] string UID)
        {
            if (scanner_courseinfo == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var Scanner_Course = await _scanner_courses.UpdateScanner_Course(scanner_courseinfo);
            if (Scanner_Course == "Ok")
            {
                return Ok();
            }
            return NotFound(Scanner_Course);
        }

        [HttpDelete]
        [Route("deleteScanner_Course/{scanner_courseId}")]
        public async Task<IActionResult> DeleteScanner_Course(uint scanner_courseId, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var scanner_course = await _scanner_courses.DeleteScanner_Course(scanner_courseId);
            if (scanner_course == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpGet]
        [Route("allCards")]
        public async Task<IActionResult> GetAllCards([FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var cards = await _cards.GetAllCards();
            if (cards.Count() == 0)
            {
                return NotFound();
            }
            return Ok(cards);
        }

        [HttpGet]
        [Route("card/{id}")]
        public async Task<IActionResult> GetCards(uint id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var card = await _cards.GetCard(id);
            if (card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost]
        [Route("addCard")]
        public async Task<IActionResult> AddCard([FromBody] Card card, [FromHeader] string UID)
        {
            if (card == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            card.isActive = true;
            var newcard = await _cards.AddCard(card);
            if (newcard != "Ok")
            {
                return NotFound(newcard);
            }
            return Ok();
        }

        [HttpPut]
        [Route("deactivateCard/{cardId}")]
        public async Task<IActionResult> DeactivateCard(uint cardId, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var card = await _cards.UpdateCardActive(cardId, false);
            if (card == null)
            {
                return NotFound("Failed to update card status");
            }
            return Ok();
        }

        [HttpGet]
        [Route("client_course/{Id}")]
        public async Task<IActionResult> GetClient_Course([FromHeader] string UID, uint Id)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var all_client_course = await _client_courses.GetAll();
            var client_course = all_client_course.FirstOrDefault(x => x.Id == Id);
            if (client_course==null)
            {
                return NotFound();
            }
            return Ok(client_course);
        }

        [HttpGet]
        [Route("client_courses/{clientId}")]
        public async Task<IActionResult> GetClient_Courses([FromHeader] string UID, uint clientId)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var client_courses = await _client_courses.GetAll();
            client_courses = client_courses.Where(x => x.ClientId == clientId).ToList();
            if (client_courses.Count() == 0)
            {
                return NotFound();
            }
            return Ok(client_courses);
        }

        [HttpGet]
        [Route("course_clients/{courseId}")]
        public async Task<IActionResult> GetCourse_clients([FromHeader] string UID, uint courseId)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var client_courses = await _client_courses.GetAll();
            client_courses = client_courses.Where(x => x.CourseId == courseId).ToList();
            if (client_courses.Count() == 0)
            {
                return NotFound();
            }
            return Ok(client_courses);
        }

        [HttpGet]
        [Route("allClient_Course")]
        public async Task<IActionResult> AllClient_Course([FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var client_courses = await _client_courses.GetAll();
            if (client_courses.Count() == 0)
            {
                return NotFound();
            }
            client_courses = client_courses.OrderBy(x => x.ClientId).ToList();
            return Ok(client_courses);
        }

        [HttpPost]
        [Route("addClient_Course")]
        public async Task<IActionResult> AddClient_Course([FromBody] Client_Course client_course, [FromHeader] string UID)
        {
            if (client_course == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var newclient_course = await _client_courses.AddClient_Course(client_course);
            if (newclient_course == "Ok")
            {
                return Ok();
            }
            return NotFound(newclient_course);

        }

        [HttpDelete]
        [Route("deleteClient_Course/{client_courseId}")]
        public async Task<IActionResult> DeleteClient_Course(uint client_courseId, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var client_course = await _client_courses.DeleteClient_Course(client_courseId);
            if (client_course == false)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
