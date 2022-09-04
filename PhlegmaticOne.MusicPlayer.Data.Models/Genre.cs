using PhlegmaticOne.MusicPlayer.Data.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.Models;

public class Genre : EntityBase, IEquatable<Genre>
{
    public string Title { get; set; } = null!;
    public ICollection<Album> Albums { get; set; } = null!;
    public override string ToString() => Title;

    public bool Equals(Genre? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Title == other.Title;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Genre) obj);
    }

    public override int GetHashCode() => HashCode.Combine(Title);
}