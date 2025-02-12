using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Admin
{
    public class AdminModel : PageModel
    {
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
