using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MVCShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        public double Total { get; set; }
        public string Status { get; set; }

        public Customer? Customer { get; set; }
        public ICollection<OrderDetail>? orderDetails { get; set; }
    }
}
