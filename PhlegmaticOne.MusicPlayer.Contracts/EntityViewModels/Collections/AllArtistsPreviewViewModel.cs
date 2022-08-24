using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;

public class AllArtistsPreviewViewModel : EntityBaseViewModel, IEntityCollection
{
    public ICollection<ArtistPreviewViewModel> Artists { get; set; }
}