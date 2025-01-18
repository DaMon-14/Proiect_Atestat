using Attendance.EF;
using Attendance.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Services
{
    public class Client_CourseService: IClient_Course
    {
        private readonly AttendanceContext _db;

        public Client_CourseService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<Client_Course>> GetClient_Courses()
        {
            return await _db.Client_Courses.Where(x => x.Id > 0).ToListAsync();
        }

        public async Task<Client_Course> GetClient_courseByClientIdAndCourseId(int clientid, int courseid)
        {
            return await _db.Client_Courses.FirstOrDefaultAsync(x => x.ClientId == clientid && x.CourseId == courseid);
        }

        public async Task<Client_Course> UpdateClient_Course(uint id, UpdateClient_Course client_course)
        {
            var client_courseToUpdate = await _db.Client_Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (client_courseToUpdate == null)
            {
                return null;
            }
            client_courseToUpdate.Points = client_course.Points;
            client_courseToUpdate.isEnrolled = client_course.isEnrolled;
            await _db.SaveChangesAsync();
            return client_courseToUpdate;
        }

        public async Task<Client_Course> AddClient_Course(int clientid, int courseid, UpdateClient_Course uclient_course)
        {
            var client = await _db.Clients.FindAsync(clientid);
            var course = await _db.Courses.FindAsync(courseid);
            if (client == null || course == null)
            {
                return null;
            }
            Client_Course client_course = new Client_Course();
            client_course.ClientId = clientid;
            client_course.CourseId = courseid;
            client_course.Points = uclient_course.Points;
            client_course.isEnrolled = uclient_course.isEnrolled;
            _db.Client_Courses.Add(client_course);
            await _db.SaveChangesAsync();
            return client_course;
        }
    }
}
