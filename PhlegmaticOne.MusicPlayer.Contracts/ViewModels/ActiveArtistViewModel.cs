using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class ActiveArtistViewModel : ArtistBaseViewModel
{
    public AlbumCover Cover { get; set; }
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
    public ICollection<AlbumPreviewViewModel> Albums { get; set; }
}