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
        public ClientController(IUser userService, IAttendance entryService)
        {
            _users = userService;
            _entries = entryService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetClient(uint id, [FromHeader] string UID)
        {
            if (UID == null)
            {
                return BadRequest();
            }
            var attendances = await _entries.GetEntriesByClientId(id, UID);
            if (attendances == null)
            {
                return NotFound();
            }
            return Ok(attendances);
        }
    }
}
