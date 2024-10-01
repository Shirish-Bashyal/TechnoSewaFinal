namespace Domain.Interfaces.Entity
{
    public interface IEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; } //  unique identifier for the entity.
    }
}
