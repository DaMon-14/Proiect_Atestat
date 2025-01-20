using Microsoft.EntityFrameworkCore;
using AttendanceAPI.Models;
using AttendanceAPI.EF;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;

namespace AttendanceAPI.Services
{
    public class CardService : ICard
    {
        private readonly AttendanceContext _db;
        public CardService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<CardDBO>> GetAllCards()
        {
            return await _db.Cards.Where(x => x.CardId > 0).ToListAsync();
        }

        public async Task<CardDBO> GetCard(uint cardid)
        {
            return await _db.Cards.FirstOrDefaultAsync(x => x.CardId == cardid);
        }

        public async Task<CardDBO> AddCard(Card card)
        {
            var newCard = new CardDBO
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

        public async Task<CardDBO> UpdateCardActive(uint cardid, bool activeState)
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
