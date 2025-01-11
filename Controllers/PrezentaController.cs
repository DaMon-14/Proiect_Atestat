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
        public IActionResult GetAll()
        {
            return Ok(_entries.GetAllEntries());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(int id)
        {
            Entry entry = _entries.GetEntryByUserId(id);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UpdateEntry addentry)
        {
            Entry entry = _entries.AddEntry(addentry);
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
        public IActionResult Put([FromRoute] int id, [FromBody] UpdateEntry addentry)
        {
            Entry entry = _entries.UpdateEntry(id, addentry);
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
        public IActionResult Delete([FromRoute] int id)
        {
            if(_entries.DeleteEntry(id) == false)
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
