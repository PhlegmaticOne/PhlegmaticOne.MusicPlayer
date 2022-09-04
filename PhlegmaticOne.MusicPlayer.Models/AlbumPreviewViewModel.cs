using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Models;

public class AlbumPreviewViewModel : CollectionBaseViewModel, IHaveYear
{
    public string AlbumType { get; set; }
    public ICollection<ArtistLinkViewModel> Artists { get; set; }
    public int YearReleased { get; set; }
}