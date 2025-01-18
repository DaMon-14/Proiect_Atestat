using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Pages.Clients
{
    public class DetailsModel : PageModel
    {
        private readonly AttendanceContext _context;

        public DetailsModel(AttendanceContext context)
        {
            _context = context;
        }

        public Client Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FirstOrDefaultAsync(m => m.ClientId == id);
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
    }
}
