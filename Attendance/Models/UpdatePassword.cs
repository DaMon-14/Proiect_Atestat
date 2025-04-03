namespace AttendanceAPI.Models
{
    public class UpdatePassword
    {
        public int ClientId { get; set; }
        public string CurrentPassword { get; set; } = default!;
        public string NewPassword { get; set; } = default!;
        public string ConfirmPassword { get; set; } = default!;
    }
}
