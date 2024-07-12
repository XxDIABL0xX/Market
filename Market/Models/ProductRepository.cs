using Microsoft.EntityFrameworkCore;

namespace Market.Models
{
    // Implementation of the IProductRepository interface
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        // Constructor injection for AppDbContext
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // Add a new product to the database
        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        // Delete a product from the database by its ID
        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Remove(product);
            _context.SaveChanges();
        }

        // Get all products with optional filtering by name, price, and product type
        public IEnumerable<Product> GetAllProduct(string Name, decimal? Price, string ProductType)
        {
            IQueryable<Product> query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(f => f.Name.Contains(Name, StringComparison.OrdinalIgnoreCase));
            }

            if (Price.HasValue)
            {
                query = query.Where(f => f.Price == Price);
            }

            if (!string.IsNullOrEmpty(ProductType))
            {
                if (Enum.TryParse<ProductType>(ProductType, true, out var parsedProductType))
                {
                    query = query.Where(f => f.productType == parsedProductType);
                }
                else
                {
                    throw new ArgumentException($"Invalid ProductType value: {ProductType}");
                }
            }

            return query.ToList();
        }

        // Get a product by its ID
        public Product? GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        // Update an existing product in the database
        public Product Update(Product productChanges)
        {
            var product = _context.Products.Attach(productChanges);
            product.State = EntityState.Modified;
            _context.SaveChanges();
            return productChanges;
        }

        // Get all products posted by a specific user
        public IEnumerable<Product> GetProductsByUser(string UserInfo)
        {
            return _context.Products.Where(p => p.UserInfo == UserInfo).ToList();
        }
    }
}
