using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

public class AllAlbumsPreviewViewModel : EntityBaseViewModel, IEntityCollection
{
    public ICollection<AlbumPreviewViewModel> Albums { get; set; }
}