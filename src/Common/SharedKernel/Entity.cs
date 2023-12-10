namespace SharedKernel;

public abstract class Entity
    : IEquatable<Entity>
{
    public virtual Guid Id { get; private init; }

    public Entity(Guid id)
    {
        this.Id = id;
    }

    public bool Equals(Entity? other)
    {
        if(other is null) 
            return false;

        return other.Id == this.Id;
    }

    public override bool Equals(object? obj)
    {
        return obj is Entity other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }
}
