using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class CollectionLinkViewModel : EntityBaseViewModel
{
    public string Title { get; set; }
    public AlbumCover Cover { get; set; }
}