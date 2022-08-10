using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class AlbumEntityViewModel : CollectionBaseViewModel
{
    public ICollection<Artist> Artists { get; set; } = null!;
    public string OnlineUrl { get; set; } = null!;
    public int YearReleased { get; set; }
    public AlbumType AlbumType { get; init; }
    public override string ToString() => $"{string.Join("/", Artists.Select(x => x.Name))} - {Title} ({YearReleased})";
}