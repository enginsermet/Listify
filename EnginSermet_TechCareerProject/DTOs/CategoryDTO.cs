using EnginSermet_TechCareerProject.Entities;

namespace EnginSermet_TechCareerProject.DTOs
{
    public class CategoryDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public IFormFile Picture { get; set; }
        public virtual Category? Category { get; set; }
    }
}
