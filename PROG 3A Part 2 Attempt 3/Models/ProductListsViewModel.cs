namespace PROG_3A_Part_2_Attempt_3.Models
{
    /// <summary>
    /// Represents a view model for displaying lists of products.
    /// </summary>
    public class ProductListsViewModel
    {
        /// <summary>
        /// Gets or sets the list of products associated with the user.
        /// </summary>
        public List<Product> UserProducts { get; set; }

        /// <summary>
        /// Gets or sets the list of products not associated with the user.
        /// </summary>
        public List<Product> OtherProducts { get; set; }
    }
}
