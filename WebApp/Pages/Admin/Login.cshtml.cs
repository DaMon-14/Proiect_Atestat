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

namespace WebApp.Pages.Login
{ 
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };
        public IActionResult OnGet()
        {
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                return RedirectToPage("./AdminInterface");
            }
            return Page();
        }

        [BindProperty]
        public PageAdmin Admin { get; set; } = default!;

        public async Task<IActionResult> OnPostLogin()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            } 

            if(Admin.Username == null || Admin.Password == null)
            {
                ModelState.AddModelError(string.Empty, "Username and password are mandatory");
                return Page();
            }
            AttendanceAPI.Models.Admin admin = new AttendanceAPI.Models.Admin
            {
                Username = Admin.Username,
                Password = Admin.Password
            };
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.PostAsync("WebApp/admin", new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.ReasonPhrase == "OK")
            {
                HttpContext.Session.SetString("Admin", admin.Username);
                return RedirectToPage("./AdminInterface");
            }

            ModelState.AddModelError(string.Empty, "Incorect username or password");
            return Page();
        }

        public async Task<IActionResult> OnPostRegister()
        {
            return RedirectToPage("./Register");
        }
    }

    public class PageAdmin
    {
        public string? Username { get; set; } = default!;
        public string? Password { get; set; } = default!;
    }
}
