using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface ICourse
    {
        //Method to get all courses
        Task<List<CourseDBO>> GetAllCourses();
        Task<List<CourseDBO>> GetActiveCourses();
        //Method to get course by id
        Task<CourseDBO> GetCourse(uint id);
        Task<CourseDBO> AddCourse(Course course);
        Task<CourseDBO> UpdateCourse(CourseDBO course);
        Task<bool> DeleteCourse(uint courseId);
    }
}
