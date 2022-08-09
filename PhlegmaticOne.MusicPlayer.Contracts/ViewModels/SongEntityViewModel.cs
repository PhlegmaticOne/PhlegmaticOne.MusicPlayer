using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class SongEntityViewModel : ObservableObject
{
    public Guid Id { get; set; }
    public string OnlineUrl { get; set; }
    public string LocalUrl { get; set; }
    public bool IsFavorite { get; set; }
    public TimeSpan TimePlayed { get; set; }
    public bool IsDownloaded { get; set; }
    public bool IsDownloading { get; set; }
    public string Title { get; set; }
    public ICollection<CollectionDisplay> Appearances { get; set; }
    public TimeSpan Duration { get; set; }
    public override string ToString() => $"{Title} - {Duration}";
}