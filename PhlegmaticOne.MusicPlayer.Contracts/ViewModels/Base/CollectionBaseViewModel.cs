using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

public class CollectionBaseViewModel : EntityBaseViewModel
{
    public AlbumCover Cover { get; set; } = null!;
    public DateTime DateAdded { get; set; }
    public string Title { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public bool IsDownloading { get; set; }
    public bool IsDownloaded { get; set; }
}