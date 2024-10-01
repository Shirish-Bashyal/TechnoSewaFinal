using Domain.Interfaces.Entity;

namespace Domain.Entities.Base
{
    public abstract class FullAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IFulAudited
    {
        protected FullAuditedEntity() { }

        public string? AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool? Deleted { get; set; }
    }
}
