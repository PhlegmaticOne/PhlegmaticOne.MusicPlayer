using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Data.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.Models;

public class Song : EntityBase, IHaveTitle, IIsFavorite, IIsDownloaded, IHaveDuration
{
    public string Title { get; set; } = null!;
    public TimeSpan Duration { get; set; }
    public Guid AlbumId { get; set; }
    public Album Album { get; set; }
    public ICollection<Artist> Artists { get; set; }
    public ICollection<Playlist> Playlists { get; set; }
    public TimeSpan TimePlayed { get; set; }
    public bool IsFavorite { get; set; }
    public string LocalUrl { get; set; }
    public string OnlineUrl { get; set; }
    public override string ToString() => $"{Title} - {Duration:g}";
    public bool IsDownloaded => string.IsNullOrEmpty(LocalUrl) == false;
}