using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF;
using Newtonsoft.Json;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace WebApp.Pages.Admin.Scanner_Courses
{
    public class DetailsModel : PageModel
    {
        public readonly IConfiguration _configuration;
        public readonly HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7172"),
        };

        public DetailsModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ScannerDBO Scanner { get; set; } = default!;

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
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/scanner/" + id.ToString());
                var scanner = JsonConvert.DeserializeObject<ScannerDBO>(await response.Content.ReadAsStringAsync());
                if (scanner == null)
                {
                    return NotFound();
                }
                Scanner = scanner;
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
