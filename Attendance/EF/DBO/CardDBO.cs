namespace AttendanceAPI.EF.DBO
{
    public class CardDBO
    {
        public int CardId { get; set; }
        public int ClientId { get; set; }
        public bool isActive { get; set; } = false;
    }
}
