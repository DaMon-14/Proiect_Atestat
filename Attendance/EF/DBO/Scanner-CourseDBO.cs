namespace AttendanceAPI.EF.DBO
{
    public class Scanner_CourseDBO
    { 
        public int Id { get; set; }
        public int ScannerId { get; set; }
        public int CourseId { get; set; }
        public bool isActive { get; set; }
    }
}
