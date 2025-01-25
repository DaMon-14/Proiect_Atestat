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

namespace WebApp.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly AttendanceContext _context;
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public EditModel(AttendanceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserDBO Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = await _context.Users.FirstOrDefaultAsync(m => m.ClientId == id);
                if (client == null)
                {
                    return NotFound();
                }
                Client = client;
                return Page();
            }
            return RedirectToPage("/Index");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                using HttpResponseMessage response = await httpClient.PutAsync("WebApp/clients", new StringContent(JsonConvert.SerializeObject(Client), Encoding.UTF8, "application/json"));
                var jsonResponse = await response.Content.ReadAsStringAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(Client.ClientId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ClientExists(int id)
        {
            return _context.Users.Any(e => e.ClientId == id);
        }
    }
}
