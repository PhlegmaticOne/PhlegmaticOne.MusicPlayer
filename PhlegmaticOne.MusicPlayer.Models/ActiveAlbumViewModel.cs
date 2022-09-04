using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Models;

public class ActiveAlbumViewModel : CollectionBaseViewModel, IHaveYear
{
    public string AlbumType { get; set; }
    public int YearReleased { get; set; }
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
    public ICollection<ArtistLinkViewModel> Artists { get; set; }
}