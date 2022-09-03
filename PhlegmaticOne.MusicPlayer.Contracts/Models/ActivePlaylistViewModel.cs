using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Contracts.Models;

public class ActivePlaylistViewModel : CollectionBaseViewModel
{
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
}