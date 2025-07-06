using Domain.Interfaces.Entity;

namespace Domain.Entities.Entity
{
    public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        protected Entity() { }

        public TPrimaryKey Id { get; set; }
    }
}
