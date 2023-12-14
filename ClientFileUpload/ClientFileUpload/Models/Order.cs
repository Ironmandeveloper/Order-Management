namespace ClientFileUpload.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
