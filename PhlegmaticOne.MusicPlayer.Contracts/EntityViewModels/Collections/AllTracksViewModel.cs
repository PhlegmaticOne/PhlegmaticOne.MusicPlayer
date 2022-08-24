using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Collections;

public class AllTracksViewModel : EntityBaseViewModel, IEntityCollection
{
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
}