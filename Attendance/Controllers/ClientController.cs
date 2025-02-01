using AttendanceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IUser _users;
        private readonly IAttendance _entries;
        private readonly IConfiguration _configuration;
        public ClientController(IUser userService, IAttendance entryService, IConfiguration configuration)
        {
            _users = userService;
            _entries = entryService;
            _configuration = configuration;
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
    }
}
