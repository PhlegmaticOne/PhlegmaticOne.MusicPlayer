using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Entities;

public class Genre : EntityBase, IEquatable<Genre>
{
    public string Name { get; set; } = null!;
    public ICollection<Album> Albums { get; set; } = null!;
    public override string ToString() => Name;

    public bool Equals(Genre? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Genre) obj);
    }

    public override int GetHashCode() => HashCode.Combine(Name);
}