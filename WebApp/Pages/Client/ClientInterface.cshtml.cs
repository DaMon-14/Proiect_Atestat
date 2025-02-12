using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AttendanceAPI.Pages.Client
{
    public class ClientModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ClientModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            
            var reps = HttpContext.Session.TryGetValue("Client", out _);
            if(reps == true)
            {
                var ClientId = Convert.ToInt32(HttpContext.Session.GetString("Client"));
                if (ClientId > 0)
                {
                    return Page();
                }
            }
            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostLogout()
        {
            HttpContext.Session.Remove("Client");
            return  RedirectToPage("/Index");
        }
    }
}
