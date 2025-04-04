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
            var userinfo = await _users.GetUserByUsername(user);
            loginResponse.Id = Convert.ToUInt32(userinfo.ClientId);
            if (userinfo.IsAdmin == false)
            {
                loginResponse.message = "Client";
            }
            else
            {
                loginResponse.message = "Admin";
            }
            return Ok(loginResponse);
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
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }

        [HttpPut]
        [Route("client")]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateUser clientinfo, [FromHeader] string UID)
        {
            if (clientinfo == null || UID != _configuration.GetValue<string>("UID"))
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


        [HttpPut]
        [Route("client/passwordUpdate")]
        public async Task<IActionResult> UpdateClientPassword([FromBody] UpdatePassword passwordChange, [FromHeader] string UID)
        {
            if (passwordChange == null || UID != _configuration.GetValue<string>("UID"))
            {
                return BadRequest();
            }
            var client = await _users.GetUser(Convert.ToUInt16(passwordChange.ClientId));
            if(client == null)
            {
                return NotFound();
            }
            var checkPassword = await _users.CorectCredentials(new UpdateUser
            {
                ClientId = passwordChange.ClientId,
                Password = passwordChange.CurrentPassword
            });
            if(checkPassword == false)
            {
                return BadRequest("Incorrect password");
            }
            UpdateUser clientInfo = new UpdateUser
            {
                ClientId = passwordChange.ClientId,
                Password = passwordChange.NewPassword
            };
            if(clientInfo.Password.Length < 8)
            {
                return BadRequest("Password must be longer than 8 caracters");
            }
            var updateClient = await _users.UpdateUser(clientInfo);
            if (updateClient == null)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
