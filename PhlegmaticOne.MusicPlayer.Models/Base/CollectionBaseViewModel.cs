using System.Drawing;
using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Models.Base;

public class CollectionBaseViewModel : EntityBaseViewModel, IHaveId, IIsFavorite, IHaveTitle, IIsDownloaded, IHaveDateAdded
{
    public Bitmap Cover { get; set; } = null!;
    public DateTime DateAdded { get; set; }
    public string Title { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public bool IsDownloading { get; set; }
    public bool IsDownloaded { get; set; }
}