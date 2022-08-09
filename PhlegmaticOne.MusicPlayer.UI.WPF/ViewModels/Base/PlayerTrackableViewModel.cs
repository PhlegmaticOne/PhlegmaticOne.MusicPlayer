using System.Windows.Forms;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

public class PlayerTrackableViewModel : BaseViewModel
{
    protected readonly IPlayer Player;
    protected readonly IObservableQueue<SongEntityViewModel> SongsQueue;
    protected readonly IValueProvider<SongEntityViewModel> SongValueProvider;
    protected readonly IValueProvider<AlbumEntityViewModel> AlbumValueProvider;
    public SongEntityViewModel CurrentSong { get; set; }
    public AlbumEntityViewModel CurrentAlbum { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;

    public PlayerTrackableViewModel(IPlayer player, IObservableQueue<SongEntityViewModel> songsQueue, 
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
        LikeCommand = new(LikeAction, _ => true);
    }
    public DelegateCommand PlaySongCommand { get; set; }
    public DelegateCommand PlayPauseCommand { get; set; }
    public DelegateCommand LikeCommand { get; set; }

    protected virtual void PlaySongAction(object? parameter)
    {
        if (parameter is SongEntityViewModel song)
        {
            SongValueProvider.Set(song);
            SongsQueue.Current = song;
            Player.Stop();
            Player.Play(ChooseFilePath(song));
        }
    }

    protected virtual void PlayPauseAction(object? parameter)
    {
        Player.PauseOrUnpause();
    }

    protected virtual void LikeAction(object? parameter)
    {
        switch (parameter)
        {
            case SongEntityViewModel song:
                song.IsFavorite = !song.IsFavorite;
                break;
            case AlbumEntityViewModel album:
                album.IsFavorite = !album.IsFavorite;
                break;
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

    protected string ChooseFilePath(SongEntityViewModel song)
    {
        return string.IsNullOrEmpty(song.LocalUrl) ? song.OnlineUrl : song.LocalUrl;
    }
}