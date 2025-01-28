using AttendanceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController
    {
        private readonly IUser _users;
        private readonly IAttendance _entries;
        public ClientController(IUser userService, IAttendance entryService)
        {
            _users = userService;
            _entries = entryService;
        }
    }
}
