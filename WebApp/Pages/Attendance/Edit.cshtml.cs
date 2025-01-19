using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Attendance.EF;
using Attendance.Models;
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Pages.Attendance
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
        public Entry Entry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using HttpResponseMessage response = await httpClient.GetAsync("WebApp/entries");
            var entries = JsonConvert.DeserializeObject<List<Entry>>(await response.Content.ReadAsStringAsync());
            var entry = entries.Where(x => x.Id == id).FirstOrDefault();
            if (entry == null)
            {
                return NotFound();
            }
            Entry = entry;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using HttpResponseMessage response = await httpClient.PutAsync("WebApp/entries", new StringContent(JsonConvert.SerializeObject(Entry), Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return RedirectToPage("./Index");
        }
    }
}
