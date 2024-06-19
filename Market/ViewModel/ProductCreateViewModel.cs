using Market.Models;
using System.ComponentModel.DataAnnotations;

namespace Market.ViewModel
{
    public class ProductCreateViewModel
    {
        [Required, MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public double Namber { get; set; }
        public ProductType? productType { get; set; }
        public string? UserInfo { get; set; }
        public IFormFile Photo { get; set; }


    }
}
