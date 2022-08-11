using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class ArtistPreviewViewModel : ArtistBaseViewModel
{
    public AlbumCover Cover { get; set; }
    public ICollection<string> Genres { get; set; }
    public int TracksCount { get; set; }
}