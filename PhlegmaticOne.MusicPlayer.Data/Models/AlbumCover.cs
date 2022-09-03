using System.Drawing;
using PhlegmaticOne.MusicPlayer.Data.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.Models;

public class AlbumCover : EntityBase
{
    public Guid AlbumId { get; set; }
    public CollectionBase Album { get; set; }
    public Bitmap Cover { get; set; }
}