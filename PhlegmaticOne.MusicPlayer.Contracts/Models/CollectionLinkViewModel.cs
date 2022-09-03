using PhlegmaticOne.MusicPlayer.Data.Models;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models;

public class CollectionLinkViewModel : EntityBaseViewModel
{
    public string Title { get; set; }
    public AlbumCover Cover { get; set; }
}