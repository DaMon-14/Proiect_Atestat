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
        public AdminController(IUser userService, IAttendance entryService)
        {
            _users = userService;
            _entries = entryService;
        }

        [HttpGet]
        [Route("clients")]
        public async Task<IActionResult> GetAllClients([FromHeader] string UID)
        {
            if(UID == null)
            {
                return BadRequest();
            }
            var clients = await _users.GetAllClients(UID);
            if(clients == null)
            {
                return NotFound();
            }
            return Ok(clients);
        }

        [HttpGet]
        [Route("client/{id}")]
        public async Task<IActionResult> GetClient(uint id, [FromHeader] string UID)
        {
            if (UID == null)
            {
                return BadRequest();
            }
            var client = await _users.GetClient(id, UID);
            if(client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPost]
        [Route("client")]
        public async Task<IActionResult> AddClient([FromBody] User client, [FromHeader] string UID)
        {
            if (client == null || UID == "")
            {
                return BadRequest();
            }

            var newclient = await _users.AddClient(client, UID);
            if(newclient == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Added Client"
            });
        }

        [HttpPut]
        [Route("client")]
        public async Task<IActionResult> UpdateClient([FromBody] User clientinfo, [FromHeader] string UID)
        {
            if(clientinfo == null || UID == "")
            {
                return BadRequest();
            }
            var client = await _users.UpdateClient(clientinfo, UID);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Updated Client",
            });
        }

        [HttpDelete("client/{id}")]
        public async Task<IActionResult> DeleteClient(uint id, [FromHeader] string UID)
        {
            if (UID == null)
            {
                return BadRequest();
            }
            var client = await _users.DeleteClient(id, UID);
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
        public async Task<IActionResult> GetAllEntries()
        {
            return Ok(await _entries.GetEntries());
        }

        [HttpGet]
        [Route("entries/{clientId}")]
        public async Task<IActionResult> GetEntriesByClientId(uint clientId)
        {
            return Ok(await _entries.GetEntriesByClientId(clientId));
        }

        [HttpGet]
        [Route("entries/{courseId}")]
        public async Task<IActionResult> GetEntriesByCourseId(uint courseId)
        {
            return Ok(await _entries.GetEntriesByCourseId(courseId));
        }

        [HttpPost]
        [Route("entries")]
        public async Task<IActionResult> AddEntry([FromBody] AttendanceDBO entry)
        {
            if (entry == null)
            {
                return BadRequest();
            }

            Attendance Entry = new Attendance
            {
                ClientId = entry.ClientId,
                CourseId = entry.CourseId,
                ScanTime = entry.ScanTime
            };

            var newclient = await _entries.AddEntry(Entry);

            return Ok(new
            {
                message = "Added Client",
                ClientId = newclient.ClientId
            });
        }

        [HttpPut]
        [Route("entries")]
        public async Task<IActionResult> UpdateEntry([FromBody] AttendanceDBO entry)
        {
            var updateEntry = await _entries.UpdateEntry(entry);
            if (updateEntry == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Updated Entry",
                EntryId = updateEntry.Id,
            });
        }

        [HttpDelete]
        [Route("entries/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var entry = await _entries.DeleteEntry(id);
            if (entry == false)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Deleted Entry",
                EntryId = id,
            });
        }
    }
}
