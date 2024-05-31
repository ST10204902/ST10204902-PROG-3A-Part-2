using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROG_3A_Part_2_Attempt_3.Models;

namespace PROG_3A_Part_2_Attempt_3.Views.Products
{
    public class CreateModel : PageModel
    {
        private readonly PROG_3A_Part_2_Attempt_3.Models.AppDbContext _context;

        public CreateModel(PROG_3A_Part_2_Attempt_3.Models.AppDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
