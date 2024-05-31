using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationRev3.Models;

namespace WebApplicationRev3.Pages
{
    [Authorize]
    public class AdminModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUser? AppUser { get; private set; }
        public bool IsAdmin { get; private set; }

        public AdminModel(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult OnGet()
        {
            LoadUser();
            if (AppUser == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            var isAdminTask = userManager.IsInRoleAsync(AppUser, "admin");
            isAdminTask.Wait();
            IsAdmin = isAdminTask.Result;

            if (!IsAdmin)
            {
                return RedirectToPage("/NotAuthorised");
            }

            return Page();
        }

        private void LoadUser()
        {
            var task = userManager.GetUserAsync(User);
            task.Wait();
            AppUser = task.Result;
        }
    }

}

