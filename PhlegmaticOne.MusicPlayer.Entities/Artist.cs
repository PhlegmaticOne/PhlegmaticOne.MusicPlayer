using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Entities;

public class Artist : EntityBase, IEquatable<Artist>
{
    public string Name { get; set; } = null!;
    public ICollection<Album> Albums { get; set; } = null!;
    public ICollection<Song> Songs { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public override string ToString() => Name;

    public bool Equals(Artist? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Artist) obj);
    }

    public override int GetHashCode() => HashCode.Combine(Name);
}