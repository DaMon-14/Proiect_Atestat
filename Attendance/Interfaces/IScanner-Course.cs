using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IScanner_Course
    {
        Task<List<Scanner_CourseDBO>> GetScanner_Courses();
        Task<Scanner_CourseDBO> GetScanner_CourseByScannerId(uint id);
        Task<string> AddScanner_Course(Scanner_Course scanner_course);
        Task<string> UpdateScanner_Course(Scanner_CourseDBO scanner_course);
        Task<bool> DeleteScanner_Course(uint id);
    }
}
