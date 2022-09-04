using System.Drawing;
using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Models;

public class ActiveArtistViewModel : ArtistBaseViewModel
{
    public Bitmap Cover { get; set; }
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
    public ICollection<AlbumPreviewViewModel> Albums { get; set; }
}