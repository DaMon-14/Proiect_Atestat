using AttendanceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using System.Globalization;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUser _users;
        private readonly IAttendance _entries;
        private readonly IConfiguration _configuration;
        public AdminController(IUser userService, IAttendance entryService, IConfiguration config)
        {
            _users = userService;
            _entries = entryService;
            _configuration = config;
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
            var client = await _users.UpdateUser(clientinfo);
            if (client == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("client/{id}")]
        public async Task<IActionResult> DeleteClient(uint id, [FromHeader] string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var client = await _users.DeleteUser(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Deleted Client",
            });
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
            if(entries == null)
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
            return NotFound(new
            {
                message = newclient
            });
        }

        [HttpPut]
        [Route("entries")]
        public async Task<IActionResult> UpdateEntry([FromBody] AttendanceDBO entry, [FromHeader] string UID)
        {
            if(entry == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }

            var updateEntry = await _entries.UpdateEntry(entry);
            if (updateEntry == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Updated Entry"
            });
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
    }
}
