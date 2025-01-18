using Attendance.Models;
using Attendance.Services;
using Microsoft.AspNetCore.Mvc;

namespace Attendance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClient _clients;
        public ClientController(IClient clientService)
        {
            _clients = clientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _clients.GetAllClients());
        }

        [HttpGet]
        [Route("get/{clientid}")]
        public async Task<IActionResult> GetClient(uint clientid)
        {
            var client = await _clients.GetClient(clientid);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
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
        [Route("update/{clientid}")]
        public async Task<IActionResult> UpdateClient(uint clientid, [FromBody] UpdateClient clientinfo)
        {
            var client = await _clients.UpdateClient(clientid, clientinfo);
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
    }
}
