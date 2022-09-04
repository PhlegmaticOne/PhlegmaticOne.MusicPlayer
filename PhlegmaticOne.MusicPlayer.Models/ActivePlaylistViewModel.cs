using PhlegmaticOne.MusicPlayer.Models.Base;

namespace PhlegmaticOne.MusicPlayer.Models;

public class ActivePlaylistViewModel : CollectionBaseViewModel
{
    public ICollection<TrackBaseViewModel> Tracks { get; set; }
}