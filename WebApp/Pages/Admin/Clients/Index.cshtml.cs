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

        public IList<ClientDBO> Client { get;set; } = default!;

        [BindProperty]
        public string errormsg { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                using HttpResponseMessage response = await httpClient.GetAsync("WebApp/clients");
                Client = JsonConvert.DeserializeObject<List<ClientDBO>>(await response.Content.ReadAsStringAsync());
            }
            if(Client == null)
            {
                Client = new List<ClientDBO>();
            }
            return Page();
        }
    }
}
