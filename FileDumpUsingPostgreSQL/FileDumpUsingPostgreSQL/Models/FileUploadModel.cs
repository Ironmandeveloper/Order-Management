namespace FileDumpUsingPostgreSQL.Models
{
    public class FileUploadModel
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
