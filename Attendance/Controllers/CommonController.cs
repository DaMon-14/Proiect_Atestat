using AttendanceAPI.Interfaces;
using AttendanceAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IUser _users;
        public CommonController(IUser userService)
        {
            _users = userService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> AdminExist([FromBody] User user, [FromHeader] string UID)
        {
            //var x =Request.Headers["z"];
            if (user == null || UID == "")
            {
                return BadRequest();
            }
            var corectlogininfo = await _users.CorectCredentials(user, UID);
            if (corectlogininfo == false)
            {
                return NotFound("Incorect Username or Password");
            }
            var adminExists = await _users.AdminExists(user, UID);
            if (adminExists == false)
            {
                return Ok("Client");
            }
            return Ok("Admin");
        }
    }
}
