using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;

public class AllAlbumsPreviewViewModel : EntityBaseViewModel, IEntityCollection
{
    public ICollection<AlbumPreviewViewModel> Albums { get; set; }
}