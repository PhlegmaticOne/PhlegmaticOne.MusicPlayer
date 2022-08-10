using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class CollectionBaseViewModel : BaseViewModel, ICollectionItem
{
    public Guid Id { get; set; }
    public ICollection<SongEntityViewModel> Songs { get; set; } = null!;
    public ICollection<GenreEntityViewModel> Genres { get; set; } = null!;
    public AlbumCover AlbumCover { get; set; } = null!;
    public string Title { get; set; } = null!;
    public DateTime DateAdded { get; set; }
    public bool IsFavorite { get; set; }
    public bool IsDownloading { get; set; }
    public bool IsDownloaded { get; set; }
}