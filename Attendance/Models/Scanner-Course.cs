namespace AttendanceAPI.Models
{
    public class Scanner_Course
    { 
        public int ScannerId { get; set; }
        public int CourseId { get; set; }

        public bool isActive { get; set; }
    }
}
