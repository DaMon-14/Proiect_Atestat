﻿using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IScanner_Course
    {
        Task<List<Scanner_Course>> GetScanner_Courses();
        Task<Scanner_Course> GetScanner_CourseByScannerId(uint id);
        Task<Scanner_Course> AddScanner_Course(UpdateScanner_Course scanner_course);
        Task<Scanner_Course> SetScanner_CourseStatus(uint id, bool isactive);
    }
}
