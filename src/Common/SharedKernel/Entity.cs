
using System.Text.Json.Serialization;

namespace SharedKernel;

public abstract class Entity<TKey>
    : IEquatable<Entity<TKey>>
    where TKey: IComparable
{
    public ID Id { get; private set; }

    public Entity(ID id)
    {
        Id = id;
    }

    public bool Equals(Entity<TKey>? other)
    {
        if (other is null)
            return false;

        return other.Id == this.Id;
    }

    public override bool Equals(object obj)
    {
        return obj is Entity<TKey> other && this.Equals(other);
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public readonly struct ID
        : IEquatable<ID>, IComparable<ID>
    {
        [JsonConstructor]
        public ID(TKey key) => this.Key = key;

        public TKey Key { get; }

        public override bool Equals(object obj)
            => this == obj as ID?;

        public bool Equals(ID other) => this.Key == null ? other.Key == null : this.Key.Equals(other.Key);

        public int CompareTo(ID other) => this.Key.CompareTo(other.Key);

        public override int GetHashCode() => this.Key.GetHashCode();

        public override string ToString()
            => this.Key.ToString() ?? throw new InvalidOperationException("Can not cast the Key to string.");

        public static bool operator !=(ID first, ID second)
            => !(first == second);

        public static bool operator ==(ID first, ID second)
            => first.Equals(second);

        public static bool operator <(ID left, ID right)
            => left.CompareTo(right) < 0;

        public static bool operator <=(ID left, ID right)
            => left.CompareTo(right) <= 0;

        public static bool operator >(ID left, ID right)
            => left.CompareTo(right) > 0;

        public static bool operator >=(ID left, ID right)
            => left.CompareTo(right) >= 0;

        public static ID Parse(string value)
            => TryParse(value, out var id)
                ? id
                : throw new InvalidOperationException("Can not cast the Key to string.");

        public static bool TryParse(string value, out ID id)
        {
            if (value is null)
            {
                id = default;
                return false;
            }

            if (typeof(TKey) == typeof(Guid) && Guid.TryParse(value, out var uuid) && uuid is TKey uuidKey)
            {
                id = new ID(uuidKey);
                return true;
            }

            if (typeof(TKey) == typeof(string) && value is TKey stringKey)
            {
                id = new ID(stringKey);
                return true;
            }

            id = default;
            return false;
        }
    }
}
