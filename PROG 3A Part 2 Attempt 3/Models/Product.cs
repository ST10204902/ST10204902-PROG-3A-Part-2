using System;
using System.Diagnostics.CodeAnalysis;

namespace PROG_3A_Part_2_Attempt_3.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public DateTime ProductionDate { get; set; }
        public string UserId { get; set; }
        
        public AppUser User { get; set; }

        [AllowNull]
        public byte[] Photo { get; set; }

        
        public double Cost { get; set; }
    }

    public enum Category
    {
        RenewableEnergy,
        WaterConservation,
        SoilHealthProducts,
        PestControl,
        Other
    }
}
