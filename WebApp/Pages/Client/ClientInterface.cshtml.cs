using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Client
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
            var reps = HttpContext.Session.TryGetValue("Admin", out _);
            if (HttpContext.Session.TryGetValue("Admin", out _))
            {
                return Page();
            }
            return RedirectToPage("/Index");
        }
        public async Task<IActionResult> OnPostLogout()
        {
            HttpContext.Session.Remove("Admin");
            return RedirectToPage("/Index");
        }
    }
}
