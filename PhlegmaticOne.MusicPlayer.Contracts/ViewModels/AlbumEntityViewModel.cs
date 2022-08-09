using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class AlbumEntityViewModel : ObservableObject
{
    public Guid Id { get; set; }
    public ICollection<SongEntityViewModel> Songs { get; set; } = null!;
    public ICollection<Genre> Genres { get; set; } = null!;
    public ICollection<Artist> Artists { get; set; } = null!;
    public AlbumCover AlbumCover { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime DateAdded { get; set; }
    public bool IsFavorite { get; set; }
    public bool IsDownloaded { get; set; }
    public string OnlineUrl { get; set; } = null!;
    public int YearReleased { get; set; }
    public AlbumType AlbumType { get; init; }
    public override string ToString() => $"{string.Join("/", Artists.Select(x => x.Name))} - {Title} ({YearReleased})";
}