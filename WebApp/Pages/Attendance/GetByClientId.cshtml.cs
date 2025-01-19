using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using AttendanceAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Pages.Attendance
{
    public class GetByClientIdModel : PageModel
    {
        private readonly AttendanceContext _context;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public GetByClientIdModel(AttendanceContext context)
        {
            _context = context;
        }

        public IList<Entry> Entries { get; set; } = default!;
        [BindProperty]
        public Entry Entry { get; set; } = new Entry();

        public async Task<IActionResult> OnGetAsync()
        {
            Entry.ClientId = 0;
            using HttpResponseMessage response = await httpClient.GetAsync("WebApp/entries");
            Entries = JsonConvert.DeserializeObject<List<Entry>>(await response.Content.ReadAsStringAsync());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using HttpResponseMessage response = await httpClient.GetAsync("WebApp/entries");
            Entries = JsonConvert.DeserializeObject<List<Entry>>(await response.Content.ReadAsStringAsync());//.Where(x=>x.ClientId == Entry.ClientId).ToList();
            Entries = Entries.Where(x => x.ClientId == Entry.ClientId).ToList();

            return Page();
        }
    }
}
