using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Core.Player;

public interface IPlayer
{
    public Song CurrentSong { get; set; }
    public void Play();
}