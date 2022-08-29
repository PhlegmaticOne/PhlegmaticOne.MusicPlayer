using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Entities.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;

public class CollectionBaseViewModel : EntityBaseViewModel, IHaveId, IIsFavorite
{
    public AlbumCover Cover { get; set; } = null!;
    public DateTime DateAdded { get; set; }
    public string Title { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public bool IsDownloading { get; set; }
    public bool IsDownloaded { get; set; }
}