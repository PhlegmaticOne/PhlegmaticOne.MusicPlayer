using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Core.Player;

public interface IPlayer
{
    public event EventHandler<TimeSpan> DurationChanged; 
    public Song CurrentSong { get; set; }
    public Album CurrentAlbum { get; set; }
    public TimeSpan CurrentDuration { get; set; }
    public void Play();
    public void Pause();
}