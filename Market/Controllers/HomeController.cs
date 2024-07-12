using Market.Models;
using Market.Roles;
using Market.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Market.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Constructor injection for IProductRepository and IWebHostEnvironment
        public HomeController(IProductRepository productRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        [HttpGet]
        // Display all products based on search criteria
        public ViewResult Index(string ProductName, decimal? Price, string ProductType)
        {
            var model = _productRepository.GetAllProduct(ProductName, Price, ProductType);
            return View(model);
        }

        [Authorize]
        [HttpGet]
        // Display the product creation form
        public ViewResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        // Handle the product creation form submission
        public IActionResult Create(ProductCreateViewModel model)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                string? uniqueFileName = null;

                // Handle file upload
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                string userInfo = User.Identity.Name ?? "Unknown User";

                // Create new product instance
                Product newproduct = new Product
                {
                    Name = model.Name,
                    Price = model.Price,
                    Namber = model.Namber,
                    productType = model.productType,
                    UserInfo = userInfo,
                    PhotoPath = uniqueFileName
                };

                // Add product to the repository
                _productRepository.Add(newproduct);
                return RedirectToAction("index");
            }

            return View();
        }

        [Authorize]
        [HttpGet]
        // Display the product edit form
        public IActionResult Edit(int id)
        {
            Product product = _productRepository.GetProduct(id);

            // Check if the product exists
            if (product == null)
            {
                return NotFound();
            }

            var productEditViewModel = new ProductEditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Namber = product.Namber,
                productType = product.productType,
                ExistingPhotoPath = product.PhotoPath
            };

            return View(productEditViewModel);
        }

        [Authorize]
        [HttpPost]
        // Handle the product edit form submission
        public IActionResult Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = _productRepository.GetProduct(model.Id);

                // Check if the user is authorized to edit the product
                if (product == null || product.UserInfo != User.Identity.Name)
                {
                    ModelState.AddModelError("", "You are not authorized to edit this product.");
                    return View(model);
                }

                // Proceed with the update if the user is authorized
                product.Name = model.Name;
                product.Price = model.Price;
                product.Namber = model.Namber;
                product.productType = model.productType;

                // Handle file upload
                if (model.Photo != null)
                {
                    if (!string.IsNullOrEmpty(model.ExistingPhotoPath))
                    {
                        string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }

                    product.PhotoPath = ProcessUploadedFile(model);
                }

                // Update product in the repository
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // Handle file upload and return the unique file name
        private string ProcessUploadedFile(ProductCreateViewModel model)
        {
            string uniqueFileName = null;

            // Handle file upload
            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }

        [Authorize]
        [HttpPost]
        // Handle product deletion
        public IActionResult Delete(int id)
        {
            _productRepository.Delete(id);
            return RedirectToAction("index");
        }

        [Authorize]
        [HttpGet]
        // Display product details
        public ViewResult Details(int id)
        {
            ProductDetaleViewModel homeDetailsViewModel = new ProductDetaleViewModel()
            {
                Product = _productRepository.GetProduct(id),
                PageTitle = "Product Details"
            };

            return View(homeDetailsViewModel);
        }
    }
}
