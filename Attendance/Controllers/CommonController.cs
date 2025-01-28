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
        LoginResponse loginResponse = new LoginResponse();
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
                loginResponse.message = "Incorect Username or Password";
                loginResponse.Id = 0;
                return NotFound(loginResponse);
            }
            var userId = await _users.GetUserId(user, UID);
            if (userId == 0)
            {
                loginResponse.message = "Failed to fetch user id";
                loginResponse.Id = 0;
                return NotFound(loginResponse);
            }
            loginResponse.Id = userId;
            var adminExists = await _users.AdminExists(user, UID);
            if (adminExists == false)
            {
                loginResponse.message = "Client";
            }
            else
            {
                loginResponse.message = "Admin";
            }
            return Ok(loginResponse);
        }
    }
}
