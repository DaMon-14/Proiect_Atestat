using Attendance.Models;

namespace Attendance.Services
{
    public interface IClient_Course
    {
        Task<List<Client_Course>> GetClient_Courses();
        Task<Client_Course> GetClient_Course(uint id);
        Task<Client_Course> UpdateClient_Course(uint id, UpdateClient_Course client_course);
        Task<Client_Course> AddClient_Course(int clientid, int courseid, UpdateClient_Course client_course);
    }
}
