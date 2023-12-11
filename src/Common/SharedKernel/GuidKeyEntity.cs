namespace SharedKernel;

public abstract class GuidKeyEntity
    : Entity<Guid>
{
    protected GuidKeyEntity(ID id) : base(id)
    {
    }
}
