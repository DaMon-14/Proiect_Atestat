using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using Newtonsoft.Json;
using System.Text;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace WebApp.Pages.Admin.Clients
{
    public class ResetPasswordModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public ResetPasswordModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        [BindProperty]
        public string Password { get; set; } = default!;
        private int Id;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                Id = Convert.ToInt32(id);
                return Page();
            }
            return RedirectToPage("/Index");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            /*
            UpdateUser Client = new UpdateUser() {
                ClientId = Id,
                Password = Password
            };


            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.PutAsync("Admin/client", new StringContent(JsonConvert.SerializeObject(Client), Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();
            
            if (response.ReasonPhrase == "OK")
            {
                return RedirectToPage("./Profile");
            }
            else if (response.ReasonPhrase == "Bad Request")
            {
                ModelState.AddModelError(string.Empty, jsonResponse);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update password");
            }
            */
            return Page();
        }

    }

    
}
