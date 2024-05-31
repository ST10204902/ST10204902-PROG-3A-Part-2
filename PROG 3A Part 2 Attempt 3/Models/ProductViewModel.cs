namespace PROG_3A_Part_2_Attempt_3.Models
{
    /// <summary>
    /// Represents a view model for displaying a product.
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the photo of the product.
        /// </summary>
        public IFormFile Photo { get; set; }
    }
}
