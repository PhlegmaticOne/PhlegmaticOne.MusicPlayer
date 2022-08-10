using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class CollectionDisplay
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public AlbumCover AlbumCover { get; set; }
}