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

namespace WebApp.Pages.LoginRegister
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
        public LoginUser User { get; set; } = default!;

        public async Task<IActionResult> OnPostLogin()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (User.Username == null || User.Password == null)
            {
                ModelState.AddModelError(string.Empty, "Id and password are mandatory");
                return Page();
            }
            User admin = new User
            {
                ClientId = 0,
                Password = User.Password,
                FirstName = "",
                LastName = "",
                Institution = "",
                Email = "",
                PhoneNumber = 0,
                UserName = User.Username
            };
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.PostAsync("/Common/Login", new StringContent(JsonConvert.SerializeObject(admin), Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();

            if (response.ReasonPhrase == "OK")
            {
                if (jsonResponse == "Admin")
                {
                    HttpContext.Session.SetString("Admin", admin.ClientId.ToString());
                    return RedirectToPage("/Admin/AdminInterface");
                }
                else if(jsonResponse == "Client")
                {
                    HttpContext.Session.SetString("Client", admin.ClientId.ToString());
                    return RedirectToPage("/Client/ClientInterface");
                }
                ModelState.AddModelError(string.Empty, "Unknown user type");
                return Page();
            }
            
            if(jsonResponse == "Incorect Username or Password")
            {
                ModelState.AddModelError(string.Empty, "Incorect Username or Password");
                return Page();
            }

            ModelState.AddModelError(string.Empty, "Unknown error");
            return Page();
        }

        public async Task<IActionResult> OnPostRegister()
        {
            return RedirectToPage("./Register");
        }
    }

    public class LoginUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
