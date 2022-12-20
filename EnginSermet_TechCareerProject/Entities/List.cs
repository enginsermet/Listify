using static NuGet.Packaging.PackagingConstants;

namespace EnginSermet_TechCareerProject.Entities
{
    public class List
    {
        public List()
        {
            ListDetails = new HashSet<ListDetail>();
        }

        public int ListId { get; set; }
        public string UserId { get; set; }
        public string ListName { get; set; }
        public virtual ICollection<ListDetail> ListDetails { get; set; }
    }
}
