using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Data.Models;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models;

public class ActiveArtistViewModel : ArtistBaseViewModel
{
    public AlbumCover Cover { get; set; }
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
    public ICollection<AlbumPreviewViewModel> Albums { get; set; }
}