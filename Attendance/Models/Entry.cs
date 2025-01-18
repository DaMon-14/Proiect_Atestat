namespace Attendance.Models
{
    public class Entry
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CourseId { get; set; }
        public DateTime ScanTime { get; set; }
    }
}
