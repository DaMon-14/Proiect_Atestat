using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using Newtonsoft.Json;
using System.Net.Http;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace WebApp.Pages.Admin.Scanner_Courses
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<Scanner_CourseDBO> Scanner_Courses { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                response = await httpClient.GetAsync("Admin/scanner_courses");
                if(response.ReasonPhrase == "Ok")
                {
                    Scanner_Courses = JsonConvert.DeserializeObject<List<Scanner_CourseDBO>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No scanner_courses found");
                    Scanner_Courses = new List<Scanner_CourseDBO>();
                }
            }
            return Page();
        }
    }
}
