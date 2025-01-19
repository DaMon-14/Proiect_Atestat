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
using System.Net.Http;

namespace WebApp.Pages.Clients
{
    public class DetailsModel : PageModel
    {
        private readonly AttendanceContext _context;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

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

            using HttpResponseMessage response = await httpClient.GetAsync("WebApp/clients");
            var clients = JsonConvert.DeserializeObject<List<Client>>(await response.Content.ReadAsStringAsync());
            var client = clients.Where(x => x.ClientId == id).FirstOrDefault();
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
