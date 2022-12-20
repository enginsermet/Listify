namespace EnginSermet_TechCareerProject.Entities
{
    public class Product
    {
        public Product()
        {
            ListDetails = new HashSet<ListDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int? CategoryId { get; set; }
        public decimal UnitPrice { get; set; }
        public byte[]? Picture { get; set; }

        public virtual Category? Category { get; set; }

        public virtual ICollection<ListDetail> ListDetails { get; set; }



    }
}
