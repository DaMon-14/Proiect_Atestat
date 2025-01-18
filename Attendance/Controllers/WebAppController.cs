using Attendance.Services;
using Attendance.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WebAppController : ControllerBase
    {
        private readonly IClient _clients;
        public WebAppController(IClient clientService)
        {
            _clients = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _clients.GetAllClients());
        }

        [HttpPost]
        public async Task<IActionResult> AddClient([FromBody] UpdateClient client)
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
        public async Task<IActionResult> UpdateClient([FromBody] Client clientinfo)
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

        [HttpDelete("{id}")]
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

    }
}
