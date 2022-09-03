using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models;

public class AlbumPreviewViewModel : CollectionBaseViewModel, ICollectionItem
{
    public AlbumType AlbumType { get; set; }
    public ICollection<ArtistLinkViewModel> Artists { get; set; }
    public int YearReleased { get; set; }
}