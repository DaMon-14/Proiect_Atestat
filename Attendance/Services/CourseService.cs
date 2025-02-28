﻿using AttendanceAPI.EF;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.Models;

namespace AttendanceAPI.Services
{
    public class CourseService : ICourse
    {
        private readonly AttendanceContext _db;
        public CourseService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<CourseDBO>> GetAllCourses()
        {
            return await _db.Courses.Where(x => x.CourseId > 0).ToListAsync();
        }

        public async Task<List<CourseDBO>> GetActiveCourses()
        {
            return await _db.Courses.Where(x => x.CourseId > 0 && x.isActive == true).ToListAsync();
        }

        public async Task<CourseDBO> GetCourse(uint courseid)
        {
            return await _db.Courses.FirstOrDefaultAsync(x => x.CourseId == courseid);
        }

        public async Task<CourseDBO> AddCourse(Course course)
        {
            var newCourse = new CourseDBO
            {
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                isActive = true
            };
            _db.Courses.Add(newCourse);
            await _db.SaveChangesAsync();
            return newCourse;
        }

        public async Task<CourseDBO> UpdateCourse(CourseDBO course)
        {
            CourseDBO? courseToUpdate = await _db.Courses.FirstOrDefaultAsync(x => x.CourseId == course.CourseId);
            if (courseToUpdate == null)
            {
                return null;
            }
            courseToUpdate.CourseName = course.CourseName;
            courseToUpdate.CourseDescription = course.CourseDescription;
            courseToUpdate.isActive = course.isActive;
            await _db.SaveChangesAsync();
            return courseToUpdate;
        }

        public async Task<bool> DeleteCourse(uint courseId)
        {
            CourseDBO? courseToDelete = await _db.Courses.FirstOrDefaultAsync(x => x.CourseId == courseId);
            if (courseToDelete == null)
            {
                return false;
            }
            courseToDelete.isActive = false;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
