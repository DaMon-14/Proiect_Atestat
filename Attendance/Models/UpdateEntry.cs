namespace Attendance.Models
{
    public class UpdateEntry
    {
        public int ClientId { get; set; }
        public int CourseId { get; set; }
        public DateTime ScanTime { get; set; }
    }
}
