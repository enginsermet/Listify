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
using EnginSermet_TechCareerProject.Data.Migrations;
using Category = EnginSermet_TechCareerProject.Entities.Category;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EnginSermet_TechCareerProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _context;

        public CategoryController(DataContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [Authorize]
        // GET: Category/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: Category/Add
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CategoryDTO categoryDTO)
        {

            if (ModelState.IsValid)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await categoryDTO.Picture.CopyToAsync(memoryStream);
                    var category = new Category
                    {
                        CategoryName = categoryDTO.CategoryName,
                        Description = categoryDTO.Description,
                        Picture = memoryStream.ToArray()
                    };
                    //category.Picture = memoryStream.ToArray();
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                }

            }
            return View(categoryDTO);
        }

        [Authorize]
        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [Authorize]
        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description,Picture")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
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
            return View(category);
        }

        [Authorize]
        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [Authorize]
        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'DataContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }

        [Route("Category/{id}/Products")]
        public async Task<IActionResult> CategoryProduct(int id)
        {
            var query = from p in _context.Products
                        where p.CategoryId == id
                        select new Product
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            UnitPrice = p.UnitPrice
                        };
            ViewBag.CategoryId = id;
            return View(query.ToList());
        }

        [Authorize]
        [Route("Category/Product/Add")]
        // GET: Category/Product/Add
        public IActionResult AddProduct(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        [Authorize]
        [Route("Category/Product/Add")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProduct(ProductDTO productDTO) 
        {
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.ProductName = productDTO.ProductName;
                product.UnitPrice = productDTO.UnitPrice;
                product.CategoryId = productDTO.CategoryId;

                product.Category = new DataContext().Categories.Find(product.CategoryId);
                _context.Add(product);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Index));
            //return View(productDTO);
        }

        [Authorize]
        [Route("List/Product/Add")]
        public async Task<IActionResult> ListProductAdd(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var query = (from l in new DataContext().Lists
                         where l.UserId == userId
                         select new List
                         {
                             ListId = l.ListId,
                             ListName = l.ListName
                         }).ToList();

            ViewBag.List = new SelectList(query, "ListId", "ListName");

            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);

            product.Category = new DataContext().Categories.Find(product.CategoryId);


            if (product == null)
            {
                return NotFound();
            }

            return View("ListProduct", product);
        }
    }
}
