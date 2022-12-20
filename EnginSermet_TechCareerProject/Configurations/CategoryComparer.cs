using EnginSermet_TechCareerProject.DTOs;
using System.Diagnostics.CodeAnalysis;

namespace EnginSermet_TechCareerProject.Configurations
{
    public class CategoryComparer : IEqualityComparer<CategoryDTO>
    {

        public bool Equals(CategoryDTO? x, CategoryDTO? y)
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }

            return x.CategoryId == y.CategoryId && x.CategoryName == y.CategoryName;
            throw new NotImplementedException();
        }

        public int GetHashCode([DisallowNull] CategoryDTO obj)
        {
            if (obj == null)
            {
                return 0;
            }

            int IDHashCode = obj.CategoryId.GetHashCode();

            int NameHashCode = obj.CategoryName == null ? 0 : obj.CategoryName.GetHashCode();
            return IDHashCode ^ NameHashCode;
        }

    }
}
