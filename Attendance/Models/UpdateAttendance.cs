namespace Attendance.Models
{
    public class UpdateAttendance
    {
        public int CourseId { get; set; }
        public int ClientId { get; set; }
        public DateTime ScanTime { get; set; }
    }
}
