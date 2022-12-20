namespace EnginSermet_TechCareerProject.DTOs
{
    public class CategoryProductDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public byte[] Picture { get; set; }
    }
}
