using Domain.Interfaces.Entity;

namespace Domain.Entities.Base
{
    public class EntityWithCreationDate<TPrimaryKey> : Entity<TPrimaryKey>, IHasCreationDate
    {
        public DateTime? AddedDate { get; set; }
    }
}
