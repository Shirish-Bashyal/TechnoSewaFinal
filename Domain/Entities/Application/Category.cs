using Domain.Entities.Entity;

namespace Domain.Entities.Application
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
