using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Commands;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

public class PlayerTrackableViewModel : BaseViewModel
{
    protected readonly IPlayer Player;
    protected readonly ISongsQueue SongsQueue;
    protected readonly IValueProvider<SongEntityViewModel> SongValueProvider;
    protected readonly IValueProvider<AlbumEntityViewModel> AlbumValueProvider;
    public SongEntityViewModel CurrentSong { get; set; }
    public AlbumEntityViewModel CurrentAlbum { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;

    public PlayerTrackableViewModel(IPlayer player, ISongsQueue songsQueue, 
        IValueProvider<SongEntityViewModel> songValueProvider, IValueProvider<AlbumEntityViewModel> albumValueProvider)
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
        LikeSongCommand = new(LikeSongAction, _ => true);
    }
    public DelegateCommand PlaySongCommand { get; set; }
    public DelegateCommand PlayPauseCommand { get; set; }
    public DelegateCommand LikeSongCommand { get; set; }

    protected virtual void PlaySongAction(object? parameter)
    {
        if (parameter is SongEntityViewModel song)
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

    protected virtual void LikeSongAction(object? parameter)
    {
        if (parameter is SongEntityViewModel song)
        {
            song.IsFavorite = !song.IsFavorite;
        }
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