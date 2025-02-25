using AttendanceAPI.EF;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using AttendanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceAPI.Services
{
    public class Client_CourseService: IClient_Course
    {
        private readonly AttendanceContext _db;

        public Client_CourseService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<Client_CourseDBO>> GetAll()
        {
            return await _db.Client_Courses.Where(x => x.Id > 0).ToListAsync();
        }

        public async Task<string> AddClient_Course(Client_Course uclient_course)
        {
            var client = await _db.Users.FindAsync(uclient_course.ClientId);
            var course = await _db.Courses.FindAsync(uclient_course.CourseId);
            if(client == null || client.IsAdmin==true) {
                return "Client not found";
            }
            if (course == null)
            {
                return "Course not found";
            }
            Client_CourseDBO client_course = new Client_CourseDBO();
            client_course.ClientId = uclient_course.ClientId;
            client_course.CourseId = uclient_course.CourseId;
            _db.Client_Courses.Add(client_course);
            await _db.SaveChangesAsync();
            return "Ok";
        }

        public async Task<bool> DeleteClient_Course(uint id)
        {
            var client_course = await _db.Client_Courses.FindAsync(Convert.ToInt32(id));
            if (client_course == null)
            {
                return false;
            }
            _db.Client_Courses.Remove(client_course);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
