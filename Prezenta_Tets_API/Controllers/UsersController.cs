using Microsoft.AspNetCore.Mvc;
using Prezenta_API.Models;
using Prezenta_API.Services;

namespace Prezenta_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUser _users;

        public UsersController(IUser users)
        {
            _users = users;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _users.GetAllUsers());
        }

        [HttpGet("userid/{id}")]
        public async Task<IActionResult> GetUserByUserId(uint id)
        {
            var entry = await _users.GetUserByUserId(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UpdateUser user)
        {
            var entry = await _users.AddUser(user);
            if (entry == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Created entry",
                id = entry.UserId
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] uint id, [FromBody] UpdateUser userinfo)
        {
            var entry = await _users.UpdateUser(id, userinfo);
            if (entry == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Updated entry",
                id = entry.UserId
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(uint id)
        {
            if (await _users.DeleteUser(id) == false)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Deleted entry",
                id = id
            });
        }
    }
}
