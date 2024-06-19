using Market.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult MyPosts()
        {
            string currentUserInfo = User.Identity.Name;

            var products = _productRepository.GetProductsByUser(currentUserInfo);

            return View(products); 
        }
    }
}
