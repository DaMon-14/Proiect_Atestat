using AttendanceAPI.Interfaces;
using AttendanceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {
        private readonly IUser _users;
        private readonly IConfiguration _configuration;
        LoginResponse loginResponse = new LoginResponse();
        public CommonController(IUser userService, IConfiguration configuration)
        {
            _users = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> UserState([FromBody] UpdateUser user, [FromHeader] string UID)
        {
            //var x =Request.Headers["z"];
            if (user == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var corectlogininfo = await _users.CorectCredentials(user);
            if (corectlogininfo == false)
            {
                loginResponse.message = "Incorect Username or Password";
                loginResponse.Id = 0;
                return NotFound(loginResponse);
            }
            var userId = await _users.GetUserId(user);
            if (userId == 0)
            {
                loginResponse.message = "Failed to fetch user id";
                loginResponse.Id = 0;
                return NotFound(loginResponse);
            }
            loginResponse.Id = userId;
            var adminExists = await _users.AdminExists(user);
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
