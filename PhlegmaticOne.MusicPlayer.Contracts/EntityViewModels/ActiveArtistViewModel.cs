using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;

public class ActiveArtistViewModel : ArtistBaseViewModel
{
    public AlbumCover Cover { get; set; }
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
    public ICollection<AlbumPreviewViewModel> Albums { get; set; }
}