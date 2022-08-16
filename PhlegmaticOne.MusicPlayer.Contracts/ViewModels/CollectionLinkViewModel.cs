using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class CollectionLinkViewModel : EntityBaseViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public AlbumCover Cover { get; set; }
}