using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using AttendanceAPI.Models;
using Newtonsoft.Json;

namespace WebApp.Pages.Clients
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

        public IList<Client> Client { get;set; } = default!;

        public async Task OnGetAsync()
        {
            using HttpResponseMessage response = await httpClient.GetAsync("WebApp/clients");
            Client = JsonConvert.DeserializeObject<List<Client>>(await response.Content.ReadAsStringAsync());
        }
    }
}
