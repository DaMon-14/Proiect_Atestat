﻿using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface ICard
    {
        Task<List<CardDBO>> GetAllCards();
        Task<CardDBO> GetCard(uint cardid);
        Task<string> AddCard(Card card);
        Task<bool> IsCardActive(uint cardid);
        Task<CardDBO> UpdateCardActive(uint cardid, bool activeState);
    }
}
