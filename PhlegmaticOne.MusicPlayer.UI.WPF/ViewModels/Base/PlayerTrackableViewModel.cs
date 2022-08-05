using PhlegmaticOne.MusicPlayer.Core.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

public class PlayerTrackableViewModel : BaseViewModel
{
    protected readonly IPlayer Player;
    protected readonly ISongsQueue SongsQueue;
    protected readonly IValueProvider<Song> SongValueProvider;
    protected readonly IValueProvider<Album> AlbumValueProvider;

    public Song CurrentSong { get; set; }
    public Album CurrentAlbum { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;

    public PlayerTrackableViewModel(IPlayer player, ISongsQueue songsQueue, IValueProvider<Song> songValueProvider, IValueProvider<Album> albumValueProvider)
    {
        Player = player;
        SongsQueue = songsQueue;
        SongValueProvider = songValueProvider;
        AlbumValueProvider = albumValueProvider;

        player.PauseChanged += (_, isPaused) => IsPaused = isPaused;
        player.StopChanged += (_, isStopped) => IsStopped = isStopped;
        SongValueProvider.ValueChanged += (_, newSong) => CurrentSong = newSong;
        AlbumValueProvider.ValueChanged += (_, newAlbum) => CurrentAlbum = newAlbum;

        PlaySongCommand = new(PlaySongAction, _ => true);
        PlayPauseCommand = new(PlayPauseAction, _ => true);
    }
    public DelegateCommand PlaySongCommand { get; set; }
    public DelegateCommand PlayPauseCommand { get; set; }

    protected virtual void PlaySongAction(object? parameter)
    {
        if (parameter is Song song)
        {
            SongValueProvider.Set(song);
            SongsQueue.Current = song;
            Player.Stop();
            Player.Play(song.OnlineUrl);
        }
    }

    protected virtual void PlayPauseAction(object? parameter)
    {
        Player.PauseOrUnpause();
    }

    protected void TrySetSong()
    {
        var song = SongValueProvider.Get();
        if (song is not null && CurrentAlbum is not null)
        {
            if (CurrentAlbum.Songs.Contains(song))
            {
                CurrentSong = song;
            }
        }
        SetIsPausedAndIsStopped();
    }

    protected void SetIsPausedAndIsStopped()
    {
        IsPaused = Player.IsPaused;
        IsStopped = Player.IsStopped;
    }
}