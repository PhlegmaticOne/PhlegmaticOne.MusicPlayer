using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class AlbumPreviewViewModel : CollectionBaseViewModel, ICollectionItem
{
    public ICollection<string> Artists { get; set; }
    public int YearReleased { get; set; }
}