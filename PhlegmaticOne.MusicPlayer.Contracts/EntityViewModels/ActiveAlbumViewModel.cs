using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;

public class ActiveAlbumViewModel : CollectionBaseViewModel
{
    public AlbumType AlbumType { get; set; }
    public int YearReleased { get; set; }
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
    public ICollection<ArtistLinkViewModel> Artists { get; set; }
}