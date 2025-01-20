namespace AttendanceAPI.EF.DBO
{
    public class ClientDBO
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Institution { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
    }
}
