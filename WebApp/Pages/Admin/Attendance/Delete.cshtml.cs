using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using AttendanceAPI.EF.DBO;

namespace WebApp.Pages.Attendance
{
    public class DeleteModel : PageModel
    {
        private readonly AttendanceContext _context;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public DeleteModel(AttendanceContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AttendanceDBO Entry { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                if (id == null)
                {
                    return NotFound();
                }
                var entry = await _context.Entries.FirstOrDefaultAsync(m => m.Id == id);

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
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using HttpResponseMessage response = await httpClient.DeleteAsync("WebApp/entries/" + id.ToString());
            var jsonResponse = await response.Content.ReadAsStringAsync();

            return RedirectToPage("./Index");
        }
    }
}
