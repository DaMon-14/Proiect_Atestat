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

namespace WebApp.Pages.Client.Courses
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

        public IList<CourseDBO> Courses { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            var reps = HttpContext.Session.TryGetValue("Client", out _);
            if (reps==true)
            {
                var id = Convert.ToInt32(HttpContext.Session.GetString("Client"));
                if (id == 0)
                {
                    return RedirectToPage("/Index");
                }
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                response = await httpClient.GetAsync("Client/courses/"+id.ToString());
                if(response.ReasonPhrase == "OK")
                {
                    Courses = JsonConvert.DeserializeObject<List<CourseDBO>>(await response.Content.ReadAsStringAsync());
                    if (Courses.Count() == 0)
                    {
                        Courses = new List<CourseDBO>();
                    }
                }
                else
                {
                    Courses = new List<CourseDBO>();
                    ModelState.AddModelError(string.Empty, "No courses found");
                }
            }
            return Page();
        }
    } 
}
