using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;

public class ActivePlaylistViewModel : CollectionBaseViewModel
{
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
}