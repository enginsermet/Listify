using Microsoft.AspNetCore.Mvc;

namespace EnginSermet_TechCareerProject.Entities
{
    public class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }

        //[BindProperty]
        //public IFormFile Picture { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
