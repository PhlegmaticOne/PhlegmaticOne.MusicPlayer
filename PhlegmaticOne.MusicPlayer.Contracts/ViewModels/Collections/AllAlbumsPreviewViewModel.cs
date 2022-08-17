using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

public class AllAlbumsPreviewViewModel : EntityBaseViewModel, IEntityCollection
{
    public ICollection<AlbumPreviewViewModel> Albums { get; set; }
}