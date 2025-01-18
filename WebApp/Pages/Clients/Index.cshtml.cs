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
    public class IndexModel : PageModel
    {
        private readonly AttendanceContext _context;

        public IndexModel(AttendanceContext context)
        {
            _context = context;
        }

        public IList<Client> Client { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Client = await _context.Client.ToListAsync();
        }
    }
}
