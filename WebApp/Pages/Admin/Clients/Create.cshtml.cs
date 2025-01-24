using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AttendanceAPI.EF;
using AttendanceAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly AttendanceContext _context;
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public CreateModel(AttendanceContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                return Page();
            }
            return RedirectToPage("/Index");
        }

        [BindProperty]
        public Client Client { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using HttpResponseMessage response = await httpClient.PostAsync("WebApp/clients", new StringContent(JsonConvert.SerializeObject(Client), Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return RedirectToPage("./Index");
        }
    }
}
