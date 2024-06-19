namespace Market.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public double Namber { get; set; }
        public ProductType? productType { get; set; }
        public string? UserInfo { get; set; }
        public string PhotoPath { get; set; }

    }
}
