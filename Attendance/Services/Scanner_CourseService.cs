using AttendanceAPI.Models;
using AttendanceAPI.EF;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AttendanceAPI.Services
{
    public class Scanner_CourseService : IScanner_Course
    {
        private readonly AttendanceContext _context;
        private readonly IScanner _scanners;
        private readonly ICourse _courses;

        public Scanner_CourseService(AttendanceContext context, IScanner scanners, ICourse course)
        {
            _context = context;
            _scanners = scanners;
            _courses = course;
        }

        public async Task<List<Scanner_CourseDBO>> GetScanner_Courses()
        {
            return await _context.Scanner_Courses.Where(x => x.Id > 0).ToListAsync();
        }

        public async Task<Scanner_CourseDBO> GetScanner_CourseByScannerId(uint id)
        {
            return await _context.Scanner_Courses.FirstOrDefaultAsync(x => x.ScannerId == id);
        }

        public async Task<string> AddScanner_Course(Scanner_Course scanner_course)
        {
            var scannerExist = await _scanners.GetScanner(Convert.ToUInt32(scanner_course.ScannerId));
            if (scannerExist == null)
            {
                return "Scanner does not exist";
            }
            var courseExist = await _courses.GetCourse(Convert.ToUInt32(scanner_course.CourseId));
            if(courseExist == null)
            {
                return "Course does not exist";
            }

            var x = await _context.Scanner_Courses.FirstOrDefaultAsync(x => x.ScannerId == scanner_course.ScannerId && x.isActive == true);
            var y = await _context.Scanner_Courses.FirstOrDefaultAsync(x => x.CourseId == scanner_course.CourseId && x.isActive == true);
            if (x != null || y != null)
            {
                return "Entry with one or more parameters already exists";
            }

            var newScanner_Course = new Scanner_CourseDBO
            {
                ScannerId = scanner_course.ScannerId,
                CourseId = scanner_course.CourseId,
                isActive = scanner_course.isActive
            };

            _context.Scanner_Courses.Add(newScanner_Course);
            await _context.SaveChangesAsync();
            return "Ok";
        }

        public async Task<string> UpdateScanner_Course(Scanner_CourseDBO scanner_courseinfo)
        {
            if(scanner_courseinfo == null)
            {
                return "Invalid input";
            }
            
            var scanner_course = await _context.Scanner_Courses.FirstOrDefaultAsync(x => x.Id == scanner_courseinfo.Id);
            if (scanner_course == null)
            {
                return "Entry does not exist";
            }

            if (scanner_courseinfo.ScannerId != 0)
            {
                var scannerExist = await _scanners.GetScanner(Convert.ToUInt32(scanner_courseinfo.ScannerId));
                if (scannerExist == null)
                {
                    return "Scanner does not exist";
                }
                scanner_course.CourseId = scanner_courseinfo.ScannerId;
            }

            if(scanner_courseinfo.CourseId != 0)
            {
                var courseExist = await _courses.GetCourse(Convert.ToUInt32(scanner_courseinfo.CourseId));
                if (courseExist == null)
                {
                    return "Course does not exist or is not active";
                }
                scanner_course.CourseId = scanner_courseinfo.CourseId;
            }

            var x = await _context.Scanner_Courses.FirstOrDefaultAsync(x => x.ScannerId == scanner_courseinfo.ScannerId && x.Id!=scanner_course.Id && x.isActive==true);
            var y = await _context.Scanner_Courses.FirstOrDefaultAsync(x => x.CourseId == scanner_courseinfo.CourseId && x.Id != scanner_course.Id && x.isActive == true);
            if (x != null || y != null)
            {
                return "Entry with one or more parameters already exists";
            }

            
            scanner_course.isActive = scanner_courseinfo.isActive;
            await _context.SaveChangesAsync();
            return "Ok";
        }

        public async Task<bool> DeleteScanner_Course(uint id)
        {
            var scanner_course = await _context.Scanner_Courses.FirstOrDefaultAsync(x => x.Id == id);
            if (scanner_course != null)
            {
                _context.Scanner_Courses.Remove(scanner_course);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
