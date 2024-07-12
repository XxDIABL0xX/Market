namespace Market.ViewModel
{
    // ViewModel for editing a product, inheriting from ProductCreateViewModel
    public class ProductEditViewModel : ProductCreateViewModel
    {
        public int Id { get; set; } // Product ID for identifying the product to edit
        public string ExistingPhotoPath { get; set; } // Path to the existing photo of the product
    }
}
