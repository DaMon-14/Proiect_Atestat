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

namespace WebApp.Pages.Admin.Scanners
{
    public class EditModel : PageModel
    {
        public readonly IConfiguration _configuration;
        private readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public EditModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public CourseDBO Course { get; set; } = default!;

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
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/course/" + id.ToString());
                var course = JsonConvert.DeserializeObject<CourseDBO>(await response.Content.ReadAsStringAsync());
                if (course == null)
                {
                    return NotFound();
                }
                Course = course;
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
            httpClient.DefaultRequestHeaders.Add("UID", _configuration.GetValue<string>("UID"));
            using HttpResponseMessage response = await httpClient.PutAsync("Admin/updateCourse", new StringContent(JsonConvert.SerializeObject(Course), Encoding.UTF8, "application/json"));
            if (response.ReasonPhrase == "OK")
            {
                return RedirectToPage("./Index");
            }
            var jsonresponse = await response.Content.ReadAsStringAsync();
            ModelState.AddModelError(string.Empty, jsonresponse);
            return Page();
        }
    }
}
