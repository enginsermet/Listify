using System.ComponentModel.DataAnnotations;

namespace EnginSermet_TechCareerProject.DTOs
{
    public class ListProductDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }
        public bool isPurchased { get; set; }

        public byte[] Picture { get; set; }
    }
}
