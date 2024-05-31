using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationRev3.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string FoodName { get; set; } = "";
        [MaxLength(100)]
        public string FoodDescription { get; set; } = "";
        [MaxLength(100)]
        [Required]
        public string FoodCategory { get; set; } = "";
        [Precision(16, 2)]
        [Required]

        public decimal FoodPrice { get; set; }
        [MaxLength(100)]
        
        public string FoodImage { get; set; } = "";
    }
}
