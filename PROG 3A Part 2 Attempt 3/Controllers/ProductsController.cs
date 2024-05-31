using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PROG_3A_Part_2_Attempt_3.Models;
using PROG_3A_Part_2_Attempt_3.Views.Products;
using System;
using System.Diagnostics;
using System.Security.Claims;

namespace PROG_3A_Part_2_Attempt_3.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            try
            {
                productViewModel.Product.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                productViewModel.Product.User = await _context.Users.FindAsync(productViewModel.Product.UserId);

                if (productViewModel.Photo != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await productViewModel.Photo.CopyToAsync(memoryStream);
                        if (memoryStream.Length < 2097152) // 2 MB
                        {
                            productViewModel.Product.Photo = memoryStream.ToArray();
                        }
                        else
                        {
                            ModelState.AddModelError("Photo", "The file is too large.");
                            return View(productViewModel);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or return an error view
                return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }

            var errors = ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .Select(x => new { x.Key, x.Value.Errors })
                .ToArray();

            if (errors.Count() == 3)
            {
                //productViewModel.Product.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                //productViewModel.Product.User = await _context.Users.FindAsync(productViewModel.Product.UserId);

                

                try
                {
                    _context.Products.Add(productViewModel.Product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Log the exception or return an error view
                    return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
                }

                return RedirectToAction("Index", "Home");
            }
            // Create a new ProductViewModel instance and set its Product property to the product
            var viewModel = new ProductViewModel { Product = productViewModel.Product };
            return View(viewModel);
        }

        /// <summary>
        /// Gets the product for editing in the webpage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        /// <summary>
        /// Edits the product when HttpPost occurs
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,Name,Category,ProductionDate,UserId,Photo,Cost")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Set the UserId to the current user's Id
                    product.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        /// <summary>
        /// Checks if a product exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}