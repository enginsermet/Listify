using EnginSermet_TechCareerProject.Data;
using EnginSermet_TechCareerProject.DTOs;
using EnginSermet_TechCareerProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EnginSermet_TechCareerProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }

        public IActionResult GetStarted()
        {
            var query = from p in _context.Products
                        join c in _context.Categories on p.CategoryId equals c.CategoryId
                        select new CategoryProductDTO
                        {
                            ProductId = p.ProductId,
                            ProductName = p.ProductName,
                            Picture = p.Picture,
                            CategoryId = c.CategoryId,
                            CategoryName = c.CategoryName,
                        };
            return View("Start", query);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Credits()
        {
            return View();
        }
    }
}