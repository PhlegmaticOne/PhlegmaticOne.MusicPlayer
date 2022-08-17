using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

public class AllTracksViewModel : EntityBaseViewModel, IEntityCollection
{
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
}