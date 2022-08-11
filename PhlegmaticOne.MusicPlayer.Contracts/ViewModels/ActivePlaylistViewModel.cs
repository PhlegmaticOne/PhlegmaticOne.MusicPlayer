using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.ViewModels;

public class ActivePlaylistViewModel : CollectionBaseViewModel
{
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
}