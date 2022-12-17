using System.ComponentModel.DataAnnotations;

namespace MVCShop.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
