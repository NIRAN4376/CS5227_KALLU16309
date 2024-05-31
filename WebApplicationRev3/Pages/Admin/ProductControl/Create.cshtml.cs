using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Protocol.Core.Types;
using WebApplicationRev3.Models;
using WebApplicationRev3.Services;

namespace WebApplicationRev3.Pages.Admin.ProductControl
{
    public class CreateModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly AppDbContext context;

        [BindProperty]
        public ProductDisplay ProductDisplay{ get; set; } = new ProductDisplay();

        public CreateModel(IWebHostEnvironment environment, AppDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet()
        {
        }

        public string errorMessage = "";
        public string successMessage = "";
        public void OnPost() 
        { 
            if (ProductDisplay.FoodImageSrc == null) 
            {
                ModelState.AddModelError("ProductDisplay.FoodImageSrc", "Please add an Image");
            }

            if (!ModelState.IsValid) 
            {
                errorMessage = "Please fill in all the fields";
                return;
            }

            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(ProductDisplay.FoodImageSrc!.FileName);

            string imageFullPath = environment.WebRootPath + "/ProductImages/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath)) 
            {
                ProductDisplay.FoodImageSrc.CopyTo(stream);
            }

            Product product = new Product()
            {
                FoodName = ProductDisplay.FoodName,
                FoodDescription = ProductDisplay.FoodDescription ?? "",
                FoodCategory = ProductDisplay.FoodCategory,
                FoodPrice = ProductDisplay.FoodPrice,
                FoodImage = newFileName
            };

            context.Products.Add(product);
            context.SaveChanges();




            ProductDisplay.FoodName = "";
            ProductDisplay.FoodDescription = "";
            ProductDisplay.FoodCategory = "";
            ProductDisplay.FoodPrice = 0;
            ProductDisplay.FoodImageSrc = null;

            ModelState.Clear();

            successMessage = "Product added";

            Response.Redirect("/admin/ProductControl/Index");
        }
    }
}
