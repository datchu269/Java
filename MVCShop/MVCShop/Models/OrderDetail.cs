namespace MVCShop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public int Qty { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public double Total { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
