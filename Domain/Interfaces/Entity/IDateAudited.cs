namespace Domain.Interfaces.Entity
{
    public interface IDateAudited : IHasCreationDate
    {
        DateTime? ModifiedDate { get; set; } // Tracks when the entity was last modified
    }
}
