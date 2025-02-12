using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AttendanceAPI.EF;
using AttendanceAPI.Models;
using Newtonsoft.Json;
using System.Text;

namespace WebApp.Pages.Admin.Scanner_Courses
{
    public class CreateModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public CreateModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> OnGet()
        {
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                return Page();
            }
            return RedirectToPage("/Index");
        }

        [BindProperty]
        public Scanner_Course Scanner_Course { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.PostAsync("Admin/addScanner_Course", new StringContent(JsonConvert.SerializeObject(Scanner_Course), Encoding.UTF8, "application/json"));
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (response.ReasonPhrase == "OK")
            {
                return RedirectToPage("./Index");
            }
            ModelState.AddModelError(string.Empty, jsonResponse);
            return Page();

        }
    }
}
