namespace Praise.Domain.Entities;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.Now;
        LastModifiedDate = null;
    }

    public Guid Id { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? LastModifiedDate { get; private set; }
}