namespace Market.Models
{
    // Interface for the product repository, defining CRUD operations and additional methods
    public interface IProductRepository
    {
        // Get a product by its ID
        Product? GetProduct(int id);

        // Get all products with optional filtering by name, price, and product type
        IEnumerable<Product> GetAllProduct(string Name, decimal? Price, string ProductType);

        // Add a new product to the repository
        Product Add(Product product);

        // Update an existing product in the repository
        Product Update(Product productChanges);

        // Delete a product from the repository by its ID
        void Delete(int id);

        // Get all products posted by a specific user
        IEnumerable<Product> GetProductsByUser(string UserInfo);
    }
}
