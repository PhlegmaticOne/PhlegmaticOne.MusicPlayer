using PhlegmaticOne.MusicPlayer.Contracts.Base;
using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models.Base;

public class CollectionBaseViewModel : EntityBaseViewModel, IHaveId, IIsFavorite
{
    public AlbumCover Cover { get; set; } = null!;
    public DateTime DateAdded { get; set; }
    public string Title { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public bool IsDownloading { get; set; }
    public bool IsDownloaded { get; set; }
}