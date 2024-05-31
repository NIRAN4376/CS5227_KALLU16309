
using System.ComponentModel.DataAnnotations;
namespace WebApplicationRev3.Models
{
    public class ProductDisplay
    {
        [MaxLength(100)]
        [Required]
        public string FoodName { get; set; } = "";
        [MaxLength(100)]
        public string? FoodDescription { get; set; } = "";
        [MaxLength(100)]
        [Required]
        public string FoodCategory { get; set; } = "";
        [Required]
        public decimal FoodPrice { get; set; }
        
        public IFormFile FoodImageSrc { get; set; } 
    }
}

