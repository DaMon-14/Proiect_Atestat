using Attendance.Models;

namespace Attendance.Services
{
    public interface ICourse
    {
        //Method to get all courses
        Task<List<Course>> GetCourses();
        //Method to get course by id
        Task<Course> GetCourse(uint id);
    }
}
