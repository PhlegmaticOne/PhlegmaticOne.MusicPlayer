using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Data.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.Models;

public class Artist : EntityBase, IEquatable<Artist>, IIsFavorite
{
    public string Title { get; set; } = null!;
    public ICollection<Album> Albums { get; set; } = null!;
    public ICollection<Song> Songs { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public override string ToString() => Title;

    public bool Equals(Artist? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Title.Equals(other.Title, StringComparison.InvariantCultureIgnoreCase);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Artist) obj);
    }

    public override int GetHashCode() => HashCode.Combine(Title);
}