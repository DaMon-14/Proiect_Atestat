﻿using AttendanceAPI.Interfaces;
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
    }
}
