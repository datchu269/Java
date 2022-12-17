namespace MVCShop.Models
{
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int PrductId { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
