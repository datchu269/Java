using System.ComponentModel.DataAnnotations;

namespace MVCShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public double Price { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
