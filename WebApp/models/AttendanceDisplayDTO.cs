namespace WebApp.models
{
    public class AttendanceDisplayDTO
    {
        public int EntryId { get; set; }
        public int ClientId { get; set; }
        public string UserName { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime ScanTime { get; set; }
    }
}
