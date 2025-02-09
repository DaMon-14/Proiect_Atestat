namespace AttendanceAPI.EF.DBO
{
    public class CourseDBO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public bool isActive { get; set; }
    }
}
