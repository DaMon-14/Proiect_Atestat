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

namespace WebApp.Pages.Admin.Attendance
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

        public AttendanceDBO Entry { get; set; } = default!;

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
                using HttpResponseMessage response = await httpClient.GetAsync("Admin/entryById/"+id.ToString());
                var entry = JsonConvert.DeserializeObject<AttendanceDBO>(await response.Content.ReadAsStringAsync());
                if (entry == null)
                {
                    return NotFound();
                }
                else
                {
                    Entry = entry;
                }
                return Page();
            }
            return RedirectToPage("/Index");
        }
    }
}
