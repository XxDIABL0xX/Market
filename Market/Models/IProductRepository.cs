namespace Market.Models
{
    public interface IProductRepository
    {
        Product? GetProduct(int id);
        IEnumerable<Product> GetAllProduct(string Name, decimal? Price, string ProductType);
        Product Add(Product product);
        Product Update(Product productChanges);
        void Delete(int id);
        IEnumerable<Product> GetProductsByUser(string UserInfo);
    }
}
