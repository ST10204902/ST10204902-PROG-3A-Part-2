using System;

namespace PROG_3A_Part_2_Attempt_3.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime ProductionDate { get; set; }
        public string UserId { get; set; }
        
        public AppUser User { get; set; }
    }
}
