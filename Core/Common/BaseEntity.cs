namespace Senswise.UserService.Core.Common;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }
}
