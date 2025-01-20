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

namespace WebApp.Pages.Attendance
{
    public class GetByCourseIdModel : PageModel
    {
        private readonly AttendanceContext _context;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public GetByCourseIdModel(AttendanceContext context)
        {
            _context = context;
        }

        public IList<AttendanceDBO> Entries { get; set; } = default!;
        [BindProperty]
        public AttendanceDBO Entry { get; set; } = new AttendanceDBO();

        public async Task<IActionResult> OnGetAsync()
        {
            Entry.ClientId = 0;
            using HttpResponseMessage response = await httpClient.GetAsync("WebApp/entries");
            Entries = JsonConvert.DeserializeObject<List<AttendanceDBO>>(await response.Content.ReadAsStringAsync());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using HttpResponseMessage response = await httpClient.GetAsync("WebApp/entries");
            Entries = JsonConvert.DeserializeObject<List<AttendanceDBO>>(await response.Content.ReadAsStringAsync());//.Where(x=>x.ClientId == Entry.ClientId).ToList();
            Entries = Entries.Where(x => x.CourseId == Entry.CourseId).ToList();

            return Page();
        }
    }
}
