namespace AttendanceAPI.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }
    }
}
