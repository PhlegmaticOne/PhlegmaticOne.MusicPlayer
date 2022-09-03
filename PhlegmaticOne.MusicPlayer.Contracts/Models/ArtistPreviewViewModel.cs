using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models;

public class ArtistPreviewViewModel : ArtistBaseViewModel, ICollectionItem
{
    public AlbumCover Cover { get; set; }
    public ICollection<string> Genres { get; set; }
    public int TracksCount { get; set; }
}