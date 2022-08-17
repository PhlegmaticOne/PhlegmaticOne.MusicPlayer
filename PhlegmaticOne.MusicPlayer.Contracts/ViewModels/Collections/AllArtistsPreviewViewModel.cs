using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

public class AllArtistsPreviewViewModel : EntityBaseViewModel, IEntityCollection
{
    public ICollection<ArtistPreviewViewModel> Artists { get; set; }
}