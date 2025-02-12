using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using AttendanceAPI.EF.DBO;
using Newtonsoft.Json;

namespace WebApp.Pages.Admin.Scanner_Courses
{
    public class DeleteModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Scanner_CourseDBO Scanner_Course { get; set; } = default!;

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
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/scanner_course/" + id.ToString());
                var scanner_course = JsonConvert.DeserializeObject<Scanner_CourseDBO>(await response.Content.ReadAsStringAsync());
                if (scanner_course == null)
                {
                    return NotFound();
                }
                else
                {
                    Scanner_Course = scanner_course;
                }
                return Page();
            }
            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.DeleteAsync("Admin/deleteScanner_Course/" + id.ToString());
            var jsonResponse = await response.Content.ReadAsStringAsync();
            if (response.ReasonPhrase != "OK")
            {
                ModelState.AddModelError(string.Empty, "Failed to delete scanner");
                return Page();
            }
            return RedirectToPage("./Index");
        }
    }
}
