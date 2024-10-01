using Domain.Interfaces.Entity;

namespace Domain.Entities.Base
{
    public class DateAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IDateAudited
    {
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
