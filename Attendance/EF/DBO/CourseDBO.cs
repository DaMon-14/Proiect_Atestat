namespace AttendanceAPI.EF.DBO
{
    public class CourseDBO
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int InstitutionId { get; set; }
        public string CourseDescription { get; set; }
    }
}
