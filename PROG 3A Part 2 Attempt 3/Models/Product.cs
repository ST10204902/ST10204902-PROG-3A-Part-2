using System;
using System.Diagnostics.CodeAnalysis;

namespace PROG_3A_Part_2_Attempt_3.Models
{
    /// <summary>
    /// Represents a product in the application.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the product ID.
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the category of the product.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the production date of the product.
        /// </summary>
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the product.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the product.
        /// </summary>
        public AppUser User { get; set; }

        /// <summary>
        /// Gets or sets the photo of the product.
        /// </summary>
        [AllowNull]
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets the cost of the product.
        /// </summary>
        public double Cost { get; set; }
    }

    /// <summary>
    /// Represents the category of a product.
    /// </summary>
    public enum Category
    {
        RenewableEnergy,
        WaterConservation,
        SoilHealthProducts,
        PestControl,
        Other
    }
}
