using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Attendance.EF;
using Attendance.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace WebApp.Pages.Attendance
{
    public class IndexModel : PageModel
    {
        private readonly AttendanceContext _context;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public IndexModel(AttendanceContext context)
        {
            _context = context;
        }

        public IList<Entry> Entry { get;set; } = default!;

        public async Task OnGetAsync()
        {
            using HttpResponseMessage response = await httpClient.GetAsync("WebApp/entries");
            Entry = JsonConvert.DeserializeObject<List<Entry>>(await response.Content.ReadAsStringAsync());
        }
    }
}
