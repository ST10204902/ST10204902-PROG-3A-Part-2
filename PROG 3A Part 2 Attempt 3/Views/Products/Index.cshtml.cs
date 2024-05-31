using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PROG_3A_Part_2_Attempt_3.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PROG_3A_Part_2_Attempt_3.Views.Products
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
            UserProducts = new List<Product>();
        }

        public IList<Product> UserProducts { get; set; }

        public async Task OnGetAsync()
        {
            var userEmail = User.Identity.Name; // Store the user email in a variable
            UserProducts = await _context.Products
                .Where(p => p.User != null && p.User.Email == userEmail) // Check if User is not null
                .ToListAsync();
        }
    }
}
