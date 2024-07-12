using Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    // Require authorization for all actions in this controller
    [Authorize] 
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        // Constructor injection for IProductRepository
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        // Display the products posted by the current user
        public IActionResult MyPosts()
        {
            // Get the current user's name
            string currentUserInfo = User.Identity.Name;

            // Retrieve products posted by the current user
            var products = _productRepository.GetProductsByUser(currentUserInfo);

            return View(products);
        }
    }
}
