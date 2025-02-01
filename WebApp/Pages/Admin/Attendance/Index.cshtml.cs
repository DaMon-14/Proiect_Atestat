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

namespace WebApp.Pages.Admin.Attendance
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

        public IList<AttendanceDBO> Entries { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/entries");
                Entries = JsonConvert.DeserializeObject<List<AttendanceDBO>>(await response.Content.ReadAsStringAsync());
                Entries = Entries.OrderBy(x => x.ScanTime.Year).ThenBy(x=>x.ScanTime.Month).ThenBy(x=>x.ScanTime.Day).ThenBy(x=>x.ScanTime.Hour).ThenBy(x=>x.ScanTime.Minute).ThenBy(x=>x.ScanTime.Second).ToList();
                Entries = Entries.Reverse().ToList();
            }
            if (Entries.Count()==0)
            {
                Entries = new List<AttendanceDBO>();
            }
            return Page();
        }
    }
}
