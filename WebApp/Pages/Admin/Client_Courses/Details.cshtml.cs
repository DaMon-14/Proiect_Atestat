using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using Newtonsoft.Json;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace WebApp.Pages.Admin.Client_Courses
{
    public class DetailsModel : PageModel
    {
        public readonly IConfiguration _configuration;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public DetailsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Scanner_CourseDBO Scanner_Course { get; set; } = default!;

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
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/scanner_course/" + id.ToString());
                var scanner_course = JsonConvert.DeserializeObject<Scanner_CourseDBO>(await response.Content.ReadAsStringAsync());
                if (scanner_course == null)
                {
                    return NotFound();
                }
                Scanner_Course = scanner_course;
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
