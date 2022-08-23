using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

public class AllArtistsPreviewViewModel : EntityBaseViewModel, IEntityCollection
{
    public ICollection<ArtistPreviewViewModel> Artists { get; set; }
}