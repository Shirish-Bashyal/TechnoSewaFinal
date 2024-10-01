namespace Domain.Interfaces.Entity
{
    public interface IHasCreationDate
    {
        public DateTime? AddedDate { get; set; } ////Tracks when the entity was Added
    }
}
