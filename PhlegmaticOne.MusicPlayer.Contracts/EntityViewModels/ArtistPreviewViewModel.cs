using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;

public class ArtistPreviewViewModel : ArtistBaseViewModel, ICollectionItem
{
    public AlbumCover Cover { get; set; }
    public ICollection<string> Genres { get; set; }
    public int TracksCount { get; set; }
}