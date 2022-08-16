using PhlegmaticOne.MusicPlayer.Entities.Base;

namespace PhlegmaticOne.MusicPlayer.Entities;

public class CollectionBase : EntityBase
{
    public ICollection<Song> Songs { get; set; } = null!;
    public AlbumCover AlbumCover { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime DateAdded { get; set; }
    public bool IsFavorite { get; set; }
    public override string ToString() => Title;
}