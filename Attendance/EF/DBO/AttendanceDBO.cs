namespace AttendanceAPI.EF.DBO
{
    public class AttendanceDBO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CourseId { get; set; }
        public DateTime ScanTime { get; set; }
    }
}
