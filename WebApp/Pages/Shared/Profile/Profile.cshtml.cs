using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace WebApp.Pages.Shared.Profile
{
    public class ProfileModel : PageModel
    {
        public readonly IConfiguration _configuration;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public ProfileModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserInfo UserInfo { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            int? id;
            if (HttpContext.Session.TryGetValue("Admin", out _) == true)
            {
                id = Convert.ToInt32(HttpContext.Session.GetString("Admin"));
                httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
                using HttpResponseMessage response = await httpClient.GetAsync("Common/client/" + id.ToString());
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(await response.Content.ReadAsStringAsync());
                if (userInfo == null)
                {
                    return NotFound();
                }
                UserInfo = userInfo;
                return Page();
            }
            else if (HttpContext.Session.TryGetValue("Client", out _) == true)
            {
                return Page();
            }

            return RedirectToPage("/Index");
        }
    }
}
