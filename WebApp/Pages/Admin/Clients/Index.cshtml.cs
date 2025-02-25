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

namespace WebApp.Pages.Admin.Clients
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public IList<UserDBO> Client { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/clients");
                if(response.ReasonPhrase == "OK")
                {
                    Client = JsonConvert.DeserializeObject<List<UserDBO>>(await response.Content.ReadAsStringAsync());
                    Client = Client.Where(x => x.IsAdmin == false).OrderBy(x => x.FirstName).ToList();
                    if (Client == null)
                    {
                        Client = new List<UserDBO>();
                    }
                }
                else
                {
                    Client = new List<UserDBO>();
                    ModelState.AddModelError(string.Empty, "No clients found");
                }
                
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
