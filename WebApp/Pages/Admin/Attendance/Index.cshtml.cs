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
        public IList<DisplayEntry> DisplayEntries { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                response = await httpClient.GetAsync("Admin/entries");
                Entries = JsonConvert.DeserializeObject<List<AttendanceDBO>>(await response.Content.ReadAsStringAsync());
                Entries = Entries.OrderBy(x => x.ScanTime.Year).ThenBy(x=>x.ScanTime.Month).ThenBy(x=>x.ScanTime.Day).ThenBy(x=>x.ScanTime.Hour).ThenBy(x=>x.ScanTime.Minute).ThenBy(x=>x.ScanTime.Second).ToList();
                Entries = Entries.Reverse().ToList();
                DisplayEntries = new List<DisplayEntry>();
                if (Entries.Count() == 0)
                {
                    return Page();
                }
                foreach (var entry in Entries)
                {
                    DisplayEntry displayEntry = new DisplayEntry();
                    response = await httpClient.GetAsync("Admin/client/" + entry.ClientId.ToString());
                    displayEntry.UserName = JsonConvert.DeserializeObject<UserDBO>(await response.Content.ReadAsStringAsync()).UserName;
                    displayEntry.ScanTime = entry.ScanTime;
                    displayEntry.CourseName = "Course Name";
                    displayEntry.ClientId = entry.ClientId;
                    DisplayEntries.Add(displayEntry);
                    //get course name from course table
                }
            }
            
            return Page();
        }
    }

    public class DisplayEntry     {
        public int ClientId { get; set; }
        public string UserName { get; set; }
        public string CourseName { get; set; }
        public DateTime ScanTime { get; set; }
    }
}
