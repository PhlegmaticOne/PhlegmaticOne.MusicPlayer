using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class ActiveAlbumViewModel : CollectionBaseViewModel
{
    public AlbumType AlbumType { get; set; }
    public int YearReleased { get; set; }
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
    public ICollection<ArtistLinkViewModel> Artists { get; set; }
}