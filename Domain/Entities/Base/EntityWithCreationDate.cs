using Domain.Interfaces.Entity;

namespace Domain.Entities.Entity
{
    public class EntityWithCreationDate<TPrimaryKey> : Entity<TPrimaryKey>, IHasCreationDate
    {
        public DateTime? AddedDate { get; set; }
    }
}
