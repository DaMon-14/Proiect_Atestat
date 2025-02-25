using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IClient_Course
    {
        Task<List<Client_CourseDBO>> GetAll();
        Task<string> AddClient_Course(Client_Course client_course);
        Task<bool> DeleteClient_Course(uint id);
    }
}
