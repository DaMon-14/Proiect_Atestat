﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using AttendanceAPI.EF.DBO;
using Newtonsoft.Json;

namespace WebApp.Pages.Admin.Courses
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public CourseDBO Course { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                if (id == null)
                {
                    return NotFound();
                }
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/course/" + id.ToString());
                var course = JsonConvert.DeserializeObject<CourseDBO>(await response.Content.ReadAsStringAsync());
                if (course == null)
                {
                    return NotFound();
                }
                else
                {
                    Course = course;
                }
                return Page();
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.DeleteAsync("Admin/deleteCourse/" + id.ToString());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if(response.ReasonPhrase != "OK")
            {
                ModelState.AddModelError(string.Empty, "Failed to delete entry");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
