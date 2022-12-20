using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EnginSermet_TechCareerProject.Data;
using EnginSermet_TechCareerProject.Entities;
using EnginSermet_TechCareerProject.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EnginSermet_TechCareerProject.Controllers
{
    public class ProductController : Controller
    {
        //private readonly DataContext _context;

        DataContext _context;

        public ProductController(/*DataContext context*/)
        {
            //_context = context;
            _context = new DataContext();
        }

        // GET: Product
        public async Task<IActionResult> Index(int id)
        {
            var query = from p in _context.Products
                        where p.CategoryId == id
                        select new Product
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            UnitPrice = p.UnitPrice
                        };
            return View(query.ToList());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);

            product.Category = _context.Categories.Find(product.CategoryId);


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = (from l in _context.Lists
                         where l.UserId == userId
                         select new List
                         {
                             ListId = l.ListId,
                             ListName = l.ListName
                         }).ToList();

            ViewBag.List = new SelectList(query, "ListId", "ListName");


            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [Authorize]
        // GET: Product/Create
        public IActionResult Add()
        {

            var categories = (from c in _context.Categories
                         select c ).ToList();

            ViewBag.CategoryList = new SelectList(categories, "CategoryId", "CategoryName");

            return View();
        }

        // POST: Product/Add

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add( ProductDTO productDTO, int CategorySelector)
        {

            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await productDTO.Picture.CopyToAsync(memoryStream);
                    Product product = new Product();
                    product.ProductName = productDTO.ProductName;
                    product.UnitPrice = productDTO.UnitPrice;
                    product.CategoryId = CategorySelector;
                    product.Picture = memoryStream.ToArray();


                    product.Category = new DataContext().Categories.Find(CategorySelector);
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                }
            }
               

                return RedirectToAction(nameof(Index));

            return View(productDTO);
        }

        [Authorize]
        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
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

        // POST: Product/Edit/5

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,CategoryId,Category,Quantity,UnitPrice")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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

        // GET: Product/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DataContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
