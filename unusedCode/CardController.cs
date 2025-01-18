using Attendance.Services;
using Microsoft.AspNetCore.Mvc;
using Attendance.Models;

namespace Attendance.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICard _cards;
        public CardController(ICard cardService)
        {
            _cards = cardService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
            return Ok(await _cards.GetAllCards());
        }

        [HttpGet]
        [Route("get/{cardid}")]
        public async Task<IActionResult> GetCard(uint cardid)
        {
            var card = await _cards.GetCard(cardid);
            if(card == null)
            {
                return NotFound();
            }
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] UpdateCard card)
        {
            if(card == null)
            {
                return BadRequest();
            }

            var newCard = await _cards.AddCard(card);
            
            return Ok(new
            {
                message = "Added Card",
                CardId = newCard.CardId
            });
        }

        [HttpGet]
        [Route("isActive/{cardid}")]
        public async Task<IActionResult> Active(uint cardid)
        {
            return Ok(await _cards.IsCardActive(cardid));
        }

        [HttpPut]
        [Route("updateActive/{cardid}")]
        public async Task<IActionResult> UpdateActive(uint cardid, [FromBody] bool activeState)
        {
            var card = await _cards.UpdateCardActive(cardid, activeState);
            if(card == null)
            {
                return NotFound();
            }
            return Ok(new
            {
                message = "Updated Card",
                CardId = card.CardId,
                isActive = card.isActive
            });
        }
    }
}
