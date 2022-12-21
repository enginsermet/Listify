using EnginSermet_TechCareerProject.Entities;

namespace EnginSermet_TechCareerProject.DTOs
{
    public class ProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? CategoryId { get; set; }

        //public int? Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public IFormFile Picture { get; set; }

        public virtual Category? Category { get; set; }

    }
}
