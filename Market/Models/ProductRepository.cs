using Microsoft.EntityFrameworkCore;

namespace Market.Models
{

    public class ProductRepository : IProductRepository
    {

        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public Product Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }

        public void Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(x => x.Id == id);
            _context.Remove(product);
            _context.SaveChanges();
        }

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


        public Product? GetProduct(int id)
        {
           return _context.Products.Find(id);
        }

        public Product Update(Product productChanges)
        {
            var product = _context.Products.Attach(productChanges);
            product.State = EntityState.Modified;
            _context.SaveChanges();
            return productChanges;
        }

        public IEnumerable<Product> GetProductsByUser(string UserInfo)
        {
            return _context.Products.Where(p => p.UserInfo == UserInfo).ToList();
        }
    }
}
