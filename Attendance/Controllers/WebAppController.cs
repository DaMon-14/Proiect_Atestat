using AttendanceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebAppController : ControllerBase
    {
        private readonly IClient _clients;
        private readonly IAttendance _entries;
        private readonly IAdmin _admins;
        public WebAppController(IClient clientService, IAttendance entryService, IAdmin adminService)
        {
            _clients = clientService;
            _entries = entryService;
            _admins = adminService;
        }

        [HttpGet]
        [Route("clients")]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _clients.GetAllClients());
        }

        [HttpPost]
        [Route("clients")]
        public async Task<IActionResult> AddClient([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            var newclient = await _clients.AddClient(client);

            return Ok(new
            {
                message = "Added Client",
                ClientId = newclient.ClientId
            });
        }

        [HttpPut]
        [Route("clients")]
        public async Task<IActionResult> UpdateClient([FromBody] ClientDBO clientinfo)
        {
            var client = await _clients.UpdateClient(clientinfo);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Updated Client",
                ClientId = client.ClientId,
            });
        }

        [HttpDelete("clients/{id}")]
        public async Task<IActionResult> DeleteClient(uint id)
        {
            var client = await _clients.DeleteClient(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Deleted Client",
                ClientId = client.ClientId,
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

        [HttpPost]
        [Route("admin")]
        public async Task<IActionResult> AdminExist([FromBody] Admin admin, [FromHeader] string UID)
        {
            //var x =Request.Headers["z"];
            if(admin == null)
            {
                return BadRequest();
            }
            var adminExists = await _admins.AdminExists(admin, UID);
            if (adminExists == false)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        [Route("addadmin")]
        public async Task<IActionResult> AddAdmin([FromBody] Admin admin, [FromHeader] string UID)
        {
            //var x =Request.Headers["z"];
            if (admin == null)
            {
                return BadRequest();
            }
            var adminExists = await _admins.AddAdmin(admin, UID);
            if (adminExists == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
