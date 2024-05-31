using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata.Ecma335;
using WebApplicationRev3.Services;

namespace WebApplicationRev3.Pages.Admin.ProductControl.ProductImages
{
    public class DeleteModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly AppDbContext context;

        public DeleteModel(IWebHostEnvironment environment, AppDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null) 
            {
                Response.Redirect("/Admin/ProductControl/Index");
                return;
            }

            var product = context.Products.Find(id);
            if (product == null) 
            {
                Response.Redirect("/Admin/ProductControl/Index");
                return;
            }

            string imageFullPath = environment.WebRootPath + "/ProductImages/" + product.FoodImage;
            System.IO.File.Delete(imageFullPath);

            context.Products.Remove(product);
            context.SaveChanges();

            Response.Redirect("/Admin/ProductControl/Index");
        }
    }
}
