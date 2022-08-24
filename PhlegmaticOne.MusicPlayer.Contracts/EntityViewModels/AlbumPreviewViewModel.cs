using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;

public class AlbumPreviewViewModel : CollectionBaseViewModel, ICollectionItem
{
    public AlbumType AlbumType { get; set; }
    public ICollection<ArtistLinkViewModel> Artists { get; set; }
    public int YearReleased { get; set; }
}