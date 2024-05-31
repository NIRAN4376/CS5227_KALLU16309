using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplicationRev3.Models;
using WebApplicationRev3.Services;

namespace WebApplicationRev3.Pages.Admin.ProductControl
{
    public class EditModel : PageModel
    {
        private readonly IWebHostEnvironment environment;
        private readonly AppDbContext context;

        [BindProperty]
        public ProductDisplay ProductDisplay { get; set; } = new ProductDisplay();
        public Product Product { get; set; } = new Product();

        public string errorMessage = "";
        public string successMessage = "";


        public EditModel(IWebHostEnvironment environment, AppDbContext context)
        {
            this.environment = environment;
            this.context = context;
        }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                Response.Redirect("Admin/ProductControl/Index");
                return;
            }

            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("Admin/ProductControl/Index");
                return;
            }

            ProductDisplay.FoodName = product.FoodName;
            ProductDisplay.FoodDescription = product.FoodDescription;
            ProductDisplay.FoodCategory = product.FoodCategory;
            ProductDisplay.FoodPrice = product.FoodPrice;

            Product = product;
        }

        public void Onpost(int? id)
        {
            if (id == null)
            {
                Response.Redirect("/Admin/ProductControl/Index");
                return;
            }

            if (!ModelState.IsValid)
            {
                errorMessage = "Please provide all the required fields";
                return;
            }

            var product = context.Products.Find(id);
            if (product == null)
            {
                Response.Redirect("/Admin/ProductControl/Index");
                return;

                string newFileName = product.FoodImage;
                if (newFileName != null)
                {
                    newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    newFileName = Path.GetExtension(ProductDisplay.FoodImageSrc.FileName);

                    string imageFullPath = environment.WebRootPath + "/ProductImages/" + newFileName;
                    using (var stream = System.IO.File.Create(imageFullPath))
                    {
                        {
                            ProductDisplay.FoodImageSrc.CopyTo(stream);
                        }

                        string oldImageFullPath = environment.WebRootPath + "/ProductImages/" + product.FoodImage;
                        System.IO.File.Delete(oldImageFullPath);
                    }

                    product.FoodName = ProductDisplay.FoodName;
                    product.FoodDescription = ProductDisplay.FoodDescription ?? "";
                    product.FoodCategory = ProductDisplay.FoodCategory;
                    product.FoodPrice = ProductDisplay.FoodPrice;
                    product.FoodImage = newFileName;

                    context.SaveChanges();


                    Product = product;

                    successMessage = "Product editted successfully";

                    Response.Redirect("/Admin/ProductControl/Index");

                }
            }

        }
    }
}
