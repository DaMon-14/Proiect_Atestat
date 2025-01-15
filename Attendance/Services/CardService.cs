using Microsoft.EntityFrameworkCore;
using Attendance.Models;
using Attendance.EF;

namespace Attendance.Services
{
    public class CardService : ICard
    {
        private readonly AttendanceContext _db;
        public CardService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<Card>> GetAllCards()
        {
            return await _db.Cards.Where(x => x.CardId > 0).ToListAsync();
        }

        public async Task<Card> GetCard(uint cardid)
        {
            return await _db.Cards.FirstOrDefaultAsync(x => x.CardId == cardid);
        }

        public async Task<Card> AddCard(UpdateCard card)
        {
            var newCard = new Card
            {
                ClientId = card.ClientId,
                isActive = card.isActive
            };
            _db.Cards.Add(newCard);
            await _db.SaveChangesAsync();
            return newCard;
        }

        public async Task<bool> IsCardActive(uint cardid)
        {
            return await _db.Cards.AnyAsync(x => x.CardId == cardid && x.isActive == true);
        }

        public async Task<Card> UpdateCardActive(uint cardid, bool activeState)
        {
            var card = await _db.Cards.FirstOrDefaultAsync(x=>x.CardId == cardid);
            if(card != null)
            {
               card.isActive = activeState;
               var results = await _db.SaveChangesAsync();
                return card;
            }
            return null;
        }
    }
}
