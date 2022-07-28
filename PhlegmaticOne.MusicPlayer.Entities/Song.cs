using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Entities;

public class Song : EntityBase
{
    public string Title { get; set; } = null!;
    public TimeSpan Duration { get; set; }
    public ICollection<CollectionBase> AlbumAppearances { get; set; } = null!;
    public TimeSpan TimePlayed { get; set; }
    public bool IsFavorite { get; set; }
    public string LocalUrl { get; set; }
    public string OnlineUrl { get; set; }
    public override string ToString() => $"{Title} - {Duration:g}";
}