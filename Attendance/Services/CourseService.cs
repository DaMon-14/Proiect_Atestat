using AttendanceAPI.EF;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AttendanceAPI.Services
{
    public class CourseService : ICourse
    {
        private readonly AttendanceContext _db;
        public CourseService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<CourseDBO>> GetCourses()
        {
            return await _db.Courses.Where(x => x.CourseId > 0).ToListAsync();
        }

        public async Task<CourseDBO> GetCourse(uint courseid)
        {
            return await _db.Courses.FirstOrDefaultAsync(x => x.CourseId == courseid);
        }
    }
}
