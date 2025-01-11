using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prezenta_API.Services;
using Prezenta_API.Models;

namespace Prezenta_API.Controllers
{
    [Route("api/[controller]")]
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
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entry = await _entries.GetEntryByUserId(id);
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
        [Route("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] UpdateEntry addentry)
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
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
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
