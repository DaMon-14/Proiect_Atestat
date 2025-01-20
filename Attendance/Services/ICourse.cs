using AttendanceAPI.EF.DBO;

namespace AttendanceAPI.Services
{
    public interface ICourse
    {
        //Method to get all courses
        Task<List<CourseDBO>> GetCourses();
        //Method to get course by id
        Task<CourseDBO> GetCourse(uint id);
    }
}
