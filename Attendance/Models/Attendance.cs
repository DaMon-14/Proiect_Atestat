namespace AttendanceAPI.Models
{
    public class Attendance
    {
        public int ClientId { get; set; }
        public int CourseId { get; set; }
        public DateTime ScanTime { get; set; }
    }
}
