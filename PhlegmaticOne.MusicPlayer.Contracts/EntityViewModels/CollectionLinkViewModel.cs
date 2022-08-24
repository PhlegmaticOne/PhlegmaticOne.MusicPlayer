using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;

public class CollectionLinkViewModel : EntityBaseViewModel
{
    public string Title { get; set; }
    public AlbumCover Cover { get; set; }
}