using AttendanceAPI.Models;
using AttendanceAPI.EF;
using Microsoft.EntityFrameworkCore;

namespace AttendanceAPI.Services
{
    public class Scanner_CourseService : IScanner_Course
    {
        private readonly AttendanceContext _context;

        public Scanner_CourseService(AttendanceContext context)
        {
            _context = context;
        }

        public async Task<List<Scanner_Course>> GetScanner_Courses()
        {
            return await _context.Scanner_Courses.Where(x => x.Id > 0).ToListAsync();
        }

        public async Task<Scanner_Course> GetScanner_CourseByScannerId(uint id)
        {
            return await _context.Scanner_Courses.FirstOrDefaultAsync(x => x.ScannerId == id);
        }

        public async Task<Scanner_Course> AddScanner_Course(UpdateScanner_Course scanner_course)
        {
            var newScanner_Course = new Scanner_Course
            {
                ScannerId = scanner_course.ScannerId,
                CourseId = scanner_course.CourseId,
            };

            _context.Scanner_Courses.Add(newScanner_Course);
            await _context.SaveChangesAsync();

            return newScanner_Course;
        }

        public async Task<Scanner_Course> SetScanner_CourseStatus(uint id, bool isactive)
        {
            var scanner_course = await _context.Scanner_Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (scanner_course != null)
            {
                scanner_course.isActive = isactive;
                await _context.SaveChangesAsync();
                return scanner_course;
            }

            return null;
        }
    }
}
