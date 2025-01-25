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
using AttendanceAPI.EF.DBO;

namespace WebApp.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public RegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };
        public IActionResult OnGet()
        {
            if(HttpContext.Session.TryGetValue("Admin", out _))
            {
               return RedirectToPage("./AdminInterface");
            }
            return Page();
        }

        [BindProperty]
        public UserDBO Admin { get; set; } = default!;

        public async Task<IActionResult> OnPostCreateAdmin()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.PostAsync("WebApp/admin", new StringContent(JsonConvert.SerializeObject(Admin), Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if(response.ReasonPhrase == "OK")
            {
                HttpContext.Session.SetString("Admin", Admin.ClientId.ToString());
                return RedirectToPage("./AdminInterface");
            }

            ModelState.AddModelError(string.Empty, "Incorect username or password");
            return Page();
        }
    }
}
