namespace Attendance.Models
{
    public class Card
    {
        public int CardId { get; set; }
        public int ClientId { get; set; }
        public bool isActive { get; set; } = false;
    }
}
