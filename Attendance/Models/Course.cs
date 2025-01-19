namespace AttendanceAPI.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int InstitutionId { get; set; }
        public string CourseDescription { get; set; }
    }
}
