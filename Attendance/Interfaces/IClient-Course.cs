using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IClient_Course
    {
        Task<List<Client_CourseDBO>> GetClient_Courses();
        Task<Client_CourseDBO> GetClient_courseByClientIdAndCourseId(int clientid, int courseid);
        Task<Client_CourseDBO> UpdateClient_Course(Client_CourseDBO client_course);
        Task<Client_CourseDBO> AddClient_Course(int clientid, int courseid, Client_Course client_course);
    }
}
