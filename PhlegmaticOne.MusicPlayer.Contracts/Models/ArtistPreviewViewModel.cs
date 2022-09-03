using System.Drawing;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models;

public class ArtistPreviewViewModel : ArtistBaseViewModel
{
    public Bitmap Cover { get; set; }
    public ICollection<string> Genres { get; set; }
    public int TracksCount { get; set; }
}