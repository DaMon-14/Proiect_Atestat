namespace Attendance.Models
{
    public class Client_Course
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CourseId { get; set; }
        public int Points { get; set;}
        public bool isEnrolled { get; set; }
    }
}
