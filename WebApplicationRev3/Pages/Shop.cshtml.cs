using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationRev3.Models;
using WebApplicationRev3.Services;

namespace WebApplicationRev3.Pages
{
    public class ShopModel : PageModel
    {
        private readonly AppDbContext context;

        public List<Product> Products { get; set; } = new List<Product>();

        public ShopModel(AppDbContext context)
        {
            this.context = context;
        }
        public void OnGet()
        {
            Products = context.Products.OrderByDescending(p => p.Id).ToList();
        }
    }
}
