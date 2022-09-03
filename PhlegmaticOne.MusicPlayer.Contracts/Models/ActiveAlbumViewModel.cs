using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models;

public class ActiveAlbumViewModel : CollectionBaseViewModel, IHaveYear
{
    public string AlbumType { get; set; }
    public int YearReleased { get; set; }
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
    public ICollection<ArtistLinkViewModel> Artists { get; set; }
}