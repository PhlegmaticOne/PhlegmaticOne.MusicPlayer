using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class TracksFromCollectionViewModel : EntityBaseViewModel
{
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
}