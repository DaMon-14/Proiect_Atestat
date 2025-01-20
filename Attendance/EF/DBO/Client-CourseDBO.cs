namespace AttendanceAPI.EF.DBO
{
    public class Client_CourseDBO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int CourseId { get; set; }
        public int Points { get; set; }
        public bool isEnrolled { get; set; }
    }
}
