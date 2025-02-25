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

namespace WebApp.Pages.Admin.Client_Courses
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

        public IList<Client_CourseDBO> Client_Courses { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                response = await httpClient.GetAsync("Admin/allClient_Course");
                if(response.ReasonPhrase == "OK")
                {
                    Client_Courses = JsonConvert.DeserializeObject<List<Client_CourseDBO>>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "No client_courses relations found");
                    Client_Courses = new List<Client_CourseDBO>();
                }
            }
            return Page();
        }
    }
}
