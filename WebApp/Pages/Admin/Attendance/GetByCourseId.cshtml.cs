using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using Newtonsoft.Json;
using System.Text;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Pages.Admin.Attendance
{
    public class GetByCourseIdModel : PageModel
    {
        private readonly IConfiguration _configuration;
        HttpResponseMessage response = new HttpResponseMessage();
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public GetByCourseIdModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<AttendanceDBO> Entries { get; set; } = default!;
        public IList<AttendanceDTO> DisplayEntries { get; set; } = default!;
        [BindProperty]
        public AttendanceDBO Entry { get; set; } = new AttendanceDBO();

        public async Task<IActionResult> OnGetAsync()
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                DisplayEntries = new List<AttendanceDTO>();
                return Page();
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            response = await httpClient.GetAsync("Admin/entryByCourse/" + Entry.CourseId.ToString());
            DisplayEntries = new List<AttendanceDTO>();
            if (response.ReasonPhrase == "OK")
            {
                Entries = JsonConvert.DeserializeObject<List<AttendanceDBO>>(await response.Content.ReadAsStringAsync());
                Entries = Entries.OrderBy(x => x.ScanTime.Year).ThenBy(x => x.ScanTime.Month).ThenBy(x => x.ScanTime.Day).ThenBy(x => x.ScanTime.Hour).ThenBy(x => x.ScanTime.Minute).ThenBy(x => x.ScanTime.Second).ToList();
                Entries = Entries.Reverse().ToList();
                if (Entries.Count() == 0)
                {
                    ModelState.AddModelError(string.Empty, "No entries found for this course");
                    return Page();
                }
                foreach (var entry in Entries)
                {
                    AttendanceDTO displayEntry = new AttendanceDTO();
                    displayEntry.ClientId = entry.ClientId;
                    response = await httpClient.GetAsync("Admin/client/" + entry.ClientId.ToString());
                    displayEntry.UserName = JsonConvert.DeserializeObject<UserDBO>(await response.Content.ReadAsStringAsync()).UserName;
                    displayEntry.ScanTime = entry.ScanTime;
                    response = await httpClient.GetAsync("Admin/course/" + entry.CourseId.ToString());
                    displayEntry.CourseName = JsonConvert.DeserializeObject<CourseDBO>(await response.Content.ReadAsStringAsync()).CourseName;
                    displayEntry.ClientId = entry.ClientId;
                    DisplayEntries.Add(displayEntry);
                    //get course name from course table
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "No entries found for this course");
            }
            return Page();
        }
    }
}
