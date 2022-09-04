using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Data.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Data.Models;

public class CollectionBase : EntityBase, IHaveTitle, IIsFavorite, IHaveDateAdded
{
    public ICollection<Song> Songs { get; set; } = null!;
    public AlbumCover AlbumCover { get; set; } = null!;
    public string Title { get; set; } = null!;
    public TimeSpan TimePlayed { get; set; }
    public DateTime DateAdded { get; set; }
    public bool IsFavorite { get; set; }
    public override string ToString() => Title;
}