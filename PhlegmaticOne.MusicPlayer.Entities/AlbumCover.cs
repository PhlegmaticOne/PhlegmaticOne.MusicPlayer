using System.Drawing;
using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Entities;

public class AlbumCover : EntityBase
{
    public Guid AlbumId { get; set; }
    public CollectionBase Album { get; set; }
    public Bitmap Cover { get; set; }
}