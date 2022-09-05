using System.Security.Cryptography.X509Certificates;
using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;

namespace PhlegmaticOne.MusicPlayer.Data.Models;

public class Album : CollectionBase, IEquatable<Album>, IHaveYear, IHaveArtistName
{
    public ICollection<Genre> Genres { get; set; } = null!;
    public ICollection<Artist> Artists { get; set; } = null!;
    public int YearReleased { get; set; }
    public AlbumType AlbumType { get; init; }
    public string ArtistName => Artists is null ? string.Empty : string.Join(' ', Artists.Select(x => x.Title));
    public override string ToString() => $"{string.Join("/", Artists.Select(x => x.Title))} - {Title} ({YearReleased})";

    public bool Equals(Album? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Artists.Equals(other.Artists) &&
               YearReleased == other.YearReleased &&
               AlbumType == other.AlbumType &&
               Title == other.Title;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Album) obj);
    }

    public override int GetHashCode() => HashCode.Combine(Artists, YearReleased, (int) AlbumType, Title);
}