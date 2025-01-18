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
using System.Text;

namespace WebApp.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly Attendance.EF.AttendanceContext _context;
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public DeleteModel(Attendance.EF.AttendanceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Client Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Clients.FirstOrDefaultAsync(m => m.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }
            else
            {
                Client = client;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using HttpResponseMessage response = await httpClient.DeleteAsync("WebApp/"+id.ToString());
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return RedirectToPage("./Index");
        }
    }
}
