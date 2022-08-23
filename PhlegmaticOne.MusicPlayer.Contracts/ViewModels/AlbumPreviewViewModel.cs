using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class AlbumPreviewViewModel : CollectionBaseViewModel, ICollectionItem
{
    public AlbumType AlbumType { get; set; }
    public ICollection<ArtistLinkViewModel> Artists { get; set; }
    public int YearReleased { get; set; }
}