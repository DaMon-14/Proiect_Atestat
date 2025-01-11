using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prezenta_API.Services;
using Prezenta_API.Models;

namespace Prezenta_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrezentaController : ControllerBase
    {
        private readonly IEntry _entries;

        public PrezentaController(IEntry entry)
        {
            _entries = entry;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _entries.GetAllEntries());
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetById(uint id)
        {
            var entry = await _entries.GetEntryById(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpGet]
        [Route("user/{userid}")]
        public async Task<IActionResult> GetByUserId(uint userid)
        {
            var entry = await _entries.GetEntryByUserId(userid);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UpdateEntry addentry)
        {
            var entry = await _entries.AddEntry(addentry);
            if (entry == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Created entry",
                id = entry.Id
            });
        }

        [HttpPut]
        [Route("id/{id}")]
        public async Task<IActionResult> Put([FromRoute] uint id, [FromBody] UpdateEntry addentry)
        {
            var entry = await _entries.UpdateEntry(id, addentry);
            if (entry == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Updated entry",
                id = entry.Id
            });
        }

        [HttpDelete]
        [Route("id/{id}")]
        public async Task<IActionResult> Delete([FromRoute] uint id)
        {
            if(await _entries.DeleteEntry(id) == false)
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
