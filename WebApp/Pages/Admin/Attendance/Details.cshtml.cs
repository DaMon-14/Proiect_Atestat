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

namespace WebApp.Pages.Attendance
{
    public class DetailsModel : PageModel
    {
        private readonly AttendanceContext _context;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public DetailsModel(AttendanceContext context)
        {
            _context = context;
        }

        public AttendanceDBO Entry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using HttpResponseMessage response = await httpClient.GetAsync("WebApp/entries");
            var entries = JsonConvert.DeserializeObject<List<AttendanceDBO>>(await response.Content.ReadAsStringAsync());
            var entry = entries.Where(x => x.Id == id).FirstOrDefault();
            if (entry == null)
            {
                return NotFound();
            }
            else
            {
                Entry = entry;
            }
            return Page();
        }
    }
}
