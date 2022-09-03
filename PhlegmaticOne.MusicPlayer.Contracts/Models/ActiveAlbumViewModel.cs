using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models;

public class ActiveAlbumViewModel : CollectionBaseViewModel
{
    public AlbumType AlbumType { get; set; }
    public int YearReleased { get; set; }
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
    public ICollection<ArtistLinkViewModel> Artists { get; set; }
}