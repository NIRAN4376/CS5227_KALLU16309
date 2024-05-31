using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplicationRev3.Models;

namespace WebApplicationRev3.Pages
{
    public class ManagerModel : PageModel
    {
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationUser? AppUser { get; private set; }
        public bool IsAdminOrManager { get; private set; }

        public ManagerModel(UserManager<ApplicationUser> userManager)
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

            bool isAdmin = Task.Run(async () => await userManager.IsInRoleAsync(AppUser, "Admin")).Result;
            bool isManager = Task.Run(async () => await userManager.IsInRoleAsync(AppUser, "Manager")).Result;

            IsAdminOrManager = isAdmin || isManager;

            if (!IsAdminOrManager)
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
