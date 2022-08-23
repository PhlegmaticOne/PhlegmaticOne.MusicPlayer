using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;

public class AllTracksViewModel : EntityBaseViewModel, IEntityCollection
{
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
}