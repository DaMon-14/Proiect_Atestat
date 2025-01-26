using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using AttendanceAPI.Models;

namespace WebApp.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public User Client { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                if (id == null)
                {
                    return NotFound();
                }

                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/client/" + id.ToString());
                var client = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());

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
            return RedirectToPage("/Index");

        }

        public async Task<IActionResult> OnPostAsync(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.DeleteAsync("Admin/client/"+id.ToString());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Unable to delete client");
            }

            return RedirectToPage("./Index");
        }
    }
}
