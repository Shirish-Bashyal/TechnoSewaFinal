using Domain.Entities.Base;

namespace Domain.Entities.Application
{
    public class Category : Entity<int>
    {
        public string Name { get; set; }
    }
}
