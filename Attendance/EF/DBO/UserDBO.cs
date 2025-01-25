namespace AttendanceAPI.EF.DBO
{
    public class UserDBO
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Institution { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Salt { get; set; } //prob will be creation date
    }
}
