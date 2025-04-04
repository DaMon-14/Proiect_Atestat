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

namespace WebApp.Pages.Shared.Profile
{
    public class ChangePasswordModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public ChangePasswordModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        [BindProperty]
        public UpdatePassword PasswordChange { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.TryGetValue("Admin", out _) || HttpContext.Session.TryGetValue("Client", out _))
            {
                PasswordChange = new UpdatePassword();
                if (id == null)
                {
                    return NotFound();
                }
                PasswordChange.ClientId = id.Value;
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
            var passwordChange = new UpdatePassword()
            {
                ClientId = PasswordChange.ClientId,
                CurrentPassword = PasswordChange.CurrentPassword,
                NewPassword = PasswordChange.NewPassword,
                ConfirmPassword = PasswordChange.ConfirmPassword
            };
            if(passwordChange.NewPassword != passwordChange.ConfirmPassword)
            {
                ModelState.AddModelError(string.Empty, "New password and confirm password do not match");
                return Page();
            }   
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.PutAsync("Common/client/passwordUpdate", new StringContent(JsonConvert.SerializeObject(PasswordChange), Encoding.UTF8, "application/json"));
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
            
            return Page();
        }
    }

    
}
