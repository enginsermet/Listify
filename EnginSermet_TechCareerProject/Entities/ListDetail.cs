using System.ComponentModel.DataAnnotations;

namespace EnginSermet_TechCareerProject.Entities
{
    public class ListDetail
    {
        public int ListId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }
        public bool isPurchased { get; set; }
        public virtual List? List { get; set; }
        public virtual Product? Product { get; set; } 
    }
}
