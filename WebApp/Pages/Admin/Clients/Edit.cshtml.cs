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

namespace AttendanceAPI.Pages.Admin.Clients
{
    public class EditModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        [BindProperty]
        public UserEdit Client { get; set; } = default!;

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
                var client = JsonConvert.DeserializeObject<UpdateUser>(await response.Content.ReadAsStringAsync());

                if (client == null)     {
                    return NotFound();
                }
                Client = new UserEdit();
                Client.Institution = client.Institution;
                Client.FirstName = client.FirstName;
                Client.LastName = client.LastName;
                Client.Email = client.Email;
                Client.PhoneNumber = client.PhoneNumber;
                Client.ClientId = client.ClientId;
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
            var User = new AttendanceAPI.Models.UpdateUser
            {
                ClientId = Client.ClientId,
                FirstName = Client.FirstName,
                LastName = Client.LastName,
                Institution = Client.Institution,
                Email = Client.Email,
                PhoneNumber = Client.PhoneNumber,
                UserName = "",
                Password = ""
            };
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.PutAsync("Admin/client", new StringContent(JsonConvert.SerializeObject(User), Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (response.ReasonPhrase == "OK")
            {
                return RedirectToPage("./Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update client");
                return Page();
            }
        }
    }


    public class UserEdit
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Institution { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
    }
}
