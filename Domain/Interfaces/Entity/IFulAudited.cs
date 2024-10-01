namespace Domain.Interfaces.Entity
{
    public interface IFulAudited : IDateAudited
    {
        public string? AddedBy { get; set; } //Tracks the id of the user who added the entity

        public string? ModifiedBy { get; set; } //Tracks the id of the user who modified the entity

        public string? DeletedBy { get; set; } //Tracks the id of the user who deleted the entity
        public DateTime? DeletedDate { get; set; } //Tracks when the entity was Deleted
        public bool? Deleted { get; set; } //Tracks whether the entity is deleted or not
    }
}
