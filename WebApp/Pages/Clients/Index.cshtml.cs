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

namespace WebApp.Pages.Clients
{
    public class IndexModel : PageModel
    {
        private readonly Attendance.EF.AttendanceContext _context;
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public IndexModel(Attendance.EF.AttendanceContext context)
        {
            _context = context;
            
        }

        public IList<Client> Client { get;set; } = default!;

        public async Task OnGetAsync()
        {
            using HttpResponseMessage response = await httpClient.GetAsync("WebApp");
            Client = JsonConvert.DeserializeObject<List<Client>>(await response.Content.ReadAsStringAsync());
        }
    }
}
