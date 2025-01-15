using Attendance.Models;

namespace Attendance.Services
{
    public interface ICard
    {
        Task<List<Card>> GetAllCards();
        Task<Card> GetCard(uint cardid);
        Task<Card> AddCard(UpdateCard card);
        Task<bool> IsCardActive(uint cardid);
        Task<Card> UpdateCardActive(uint cardid, bool activeState);
    }
}
