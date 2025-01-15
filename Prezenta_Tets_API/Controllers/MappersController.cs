using Microsoft.AspNetCore.Mvc;
using Prezenta_API.Models;
using Prezenta_API.Services;

namespace Prezenta_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MappersController : ControllerBase
    {
        private readonly IMapper _mappers;

        public MappersController(IMapper mapper)
        {
            _mappers = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mappers.GetAllMappers());
        }

        [HttpGet]
        [Route("user/{usercode}")]
        public async Task<IActionResult> GetByUserId(uint usercode)
        {
            var entry = await _mappers.GetMapperByUserCode(usercode);
            if (entry == null)
            {
                return NotFound();
            }
            return Ok(entry);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UpdateMapper addmap)
        {
            var map = await _mappers.AddMapper(addmap);
            if (map == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                message = "Created entry",
                id = map.UserId
            });
        }

        [HttpPut]
        [Route("user/{usercode}")]
        public async Task<IActionResult> Put([FromRoute] uint usercode, [FromBody] UpdateMapper addmapper)
        {
            var map = await _mappers.SetActiveMap(usercode, addmapper.isActive);
            if (map == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                message = "Updated entry",
                id = map.UserCode
            });
        }

        [HttpDelete]
        [Route("user/{usercode}")]
        public async Task<IActionResult> Delete([FromRoute] uint usercode)
        {
            if (await _mappers.DeleteMapper(usercode) == false)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Deleted entry",
                id = usercode
            });
        }
    }
}
