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

namespace WebApp.Pages.Client.Attendance
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        HttpResponseMessage response = new HttpResponseMessage();
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IList<AttendanceAPI.Models.Attendance> Entries { get; set; } = default!;
        public IList<AttendanceDTO> DisplayEntries { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var reps = HttpContext.Session.TryGetValue("Client", out _);
            if (reps == true)
            {
                var id = Convert.ToInt32(HttpContext.Session.GetString("Client"));
                if (id == 0)
                {
                    return RedirectToPage("/Index");
                }
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                response = await httpClient.GetAsync("Client/" + id.ToString());
                if (response.ReasonPhrase == "OK")
                {
                    Entries = JsonConvert.DeserializeObject<List<AttendanceAPI.Models.Attendance>>(await response.Content.ReadAsStringAsync());
                    Entries = Entries.OrderBy(x => x.ScanTime.Year).ThenBy(x => x.ScanTime.Month).ThenBy(x => x.ScanTime.Day).ThenBy(x => x.ScanTime.Hour).ThenBy(x => x.ScanTime.Minute).ThenBy(x => x.ScanTime.Second).ToList();
                    Entries = Entries.Reverse().ToList();
                    DisplayEntries = new List<AttendanceDTO>();
                    if (Entries.Count() == 0)
                    {
                        return Page();
                    }
                    foreach (var entry in Entries)
                    {
                        AttendanceDTO displayEntry = new AttendanceDTO();
                        response = await httpClient.GetAsync("Client/client/" + entry.ClientId.ToString());
                        displayEntry.UserName = JsonConvert.DeserializeObject<UserDBO>(await response.Content.ReadAsStringAsync()).UserName;
                        displayEntry.ScanTime = entry.ScanTime;
                        response = await httpClient.GetAsync("Client/course/" + entry.CourseId.ToString());
                        displayEntry.CourseName = JsonConvert.DeserializeObject<CourseDBO>(await response.Content.ReadAsStringAsync()).CourseName;
                        DisplayEntries.Add(displayEntry);
                    }
                }
                else
                {
                    DisplayEntries= new List<AttendanceDTO>();
                }
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
