namespace Market.Models
{
    // Class representing a product entity in the application
    public class Product
    {
        // Unique identifier for the product
        public int Id { get; set; }

        // Name of the product
        public string Name { get; set; }

        // Price of the product
        public decimal Price { get; set; }

        // Quantity or number of items (consider renaming 'Namber' to 'Number' if this is a typo)
        public double Namber { get; set; }

        // Type of the product (nullable)
        public ProductType? productType { get; set; }

        // Information about the user who posted the product (nullable)
        public string? UserInfo { get; set; }

        // Path to the photo of the product
        public string PhotoPath { get; set; }
    }
}
