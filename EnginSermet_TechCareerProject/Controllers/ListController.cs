using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using EnginSermet_TechCareerProject.Data;
using EnginSermet_TechCareerProject.Entities;
using EnginSermet_TechCareerProject.DTOs;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace EnginSermet_TechCareerProject.Controllers
{
    [Authorize]
    public class ListController : Controller
    {

        private readonly DataContext _context;

        public ListController(DataContext context)
        {
            _context = context;
        }

        // GET: List
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(await _context.Lists.Where(a => a.UserId == userId).ToListAsync());
        }

        // GET: List/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var list = await _context.Lists
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        // GET: List/Add
        public IActionResult Add()
        {
            return View();
        }

        // POST: List/Add

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ListDTO listDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                List list = new List();
                list.UserId = userId;
                list.ListName = listDTO.ListName;

                _context.Add(list);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: List/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var list = await _context.Lists.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }
            return View(list);
        }

        // POST: List/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ListId,UserId,ListName")] List list)
        {
            if (id != list.ListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(list);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListExists(list.ListId))
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
            return View(list);
        }

        // GET: List/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lists == null)
            {
                return NotFound();
            }

            var list = await _context.Lists
                .FirstOrDefaultAsync(m => m.ListId == id);
            if (list == null)
            {
                return NotFound();
            }

            return View(list);
        }

        // POST: List/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lists == null)
            {
                return Problem("Entity set 'DataContext.Lists'  is null.");
            }
            var list = await _context.Lists.FindAsync(id);
            if (list != null)
            {
                _context.Lists.Remove(list);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListExists(int id)
        {
          return _context.Lists.Any(e => e.ListId == id);
        }


        [HttpPost]
        public IActionResult AddProduct(int productId, int ListSelector)
        {
            ListDetail listDetail = new ListDetail();
            listDetail.ListId = ListSelector;
            listDetail.ProductId = productId;
            listDetail.Quantity = 1;
            listDetail.Description = "";
            listDetail.isPurchased = false;
            _context.ListDetails.Add(listDetail);
            _context.SaveChanges();

            var query = (from d in new DataContext().ListDetails
                         where d.ListId == ListSelector
                         select new ListProductDTO
                         {
                             ProductName = d.Product.ProductName,
                             UnitPrice = d.Product.UnitPrice,
                             Quantity = d.Quantity,
                             Description = d.Description
                         });

            var routeValues = new RouteValueDictionary();
            routeValues.Add("id", listDetail.ListId);

            return RedirectToAction("ListDetails", routeValues);

        }


        
        [Route("List/Details")]
        public async Task<IActionResult> ListDetails(int id)
        {
            DataContext Context = new DataContext();

            ViewBag.ListId = id;

            var query = (from d in _context.ListDetails
                        where d.ListId == id
                        select new ListProductDTO
                        {
                            ProductId = d.ProductId,
                            ProductName = d.Product.ProductName,
                            UnitPrice = d.Product.UnitPrice,
                            Quantity = d.Quantity,
                            Description = d.Description,
                        });

            var list = await Context.Lists.SingleOrDefaultAsync(l => l.ListId == id);
            ViewBag.ListName = list.ListName;

            return View(query.ToList());
        }

        [Route("List/Product/{id}/Details")]
        public async Task<IActionResult> ListProductDetails(int id, int listId)
        {
            DataContext Context = new DataContext();

            var product = await Context.Products.SingleOrDefaultAsync(p => p.ProductId == id);

            ViewBag.ProductName = product.ProductName;
            ViewBag.UnitPrice = product.UnitPrice;
            ViewBag.Picture = product.Picture;

            var listProductDetail = await _context.ListDetails.SingleOrDefaultAsync(a => a.ListId == listId && a.ProductId == id );

            return View(listProductDetail);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ListProductUpdate( ListDetail listDetail)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListExists(listDetail.ListId))
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
            return View(listDetail);
        }


        [HttpPost]
        [Route("List/Product/Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ListProductDelete(int id, int listId)
        {
            if (_context.ListDetails == null)
            {
                return Problem("Entity set 'DataContext.Lists'  is null.");
            }
            var listDetail = await _context.ListDetails.SingleOrDefaultAsync(a => a.ListId == listId && a.ProductId == id);
            if (listDetail != null)
            {
                _context.ListDetails.Remove(listDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Shopping(int id)
        {
            var query = (from d in _context.ListDetails
                         where d.ListId == id
                         select new ListProductDTO
                         {
                             ProductId = d.Product.ProductId,
                             ProductName = d.Product.ProductName,
                             UnitPrice = d.Product.UnitPrice,
                             Quantity = d.Quantity,
                             Description = d.Description,
                             isPurchased = d.isPurchased                           
                        });

            return View(query.ToList());
        }

        // Did not work as intented
        //public async Task<IActionResult> PurchaseProduct(int productId)
        //{
        //    return View("ListDetails");
        //}

        //[HttpPatch("{productId}")]
        //public async Task<IActionResult> PurchaseProduct(int productId, [FromBody] JsonPatchDocument<ListDetail> jsonPatch )
        //{
        //    if (jsonPatch != null)
        //    {
        //        var product = await _context.ListDetails
        //            .FirstOrDefaultAsync(m => m.ProductId == productId);

        //        product.isPurchased = true;

        //        jsonPatch.ApplyTo(product, ModelState);


        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        return new ObjectResult(product);
        //    }
        //    return View("ListDetails");
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PurchaseProduct(int productId)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _context.ListDetails
                    .FirstOrDefaultAsync(m => m.ProductId == productId);
                    product.isPurchased = true;

                    var listId = product.ListId;
                    var routeValues = new RouteValueDictionary();
                    routeValues.Add("id", listId);
                    
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Shopping", routeValues);
                }
                catch (DbUpdateConcurrencyException)
                {
                        throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UndoPurchase(int productId)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _context.ListDetails
                    .FirstOrDefaultAsync(m => m.ProductId == productId);
                    product.isPurchased = false;

                    var listId = product.ListId;
                    var routeValues = new RouteValueDictionary();
                    routeValues.Add("id", listId);

                    await _context.SaveChangesAsync();

                    return RedirectToAction("Shopping", routeValues);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}



