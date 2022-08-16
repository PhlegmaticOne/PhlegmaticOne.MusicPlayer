using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

public class PlayerTrackableViewModel : ApplicationBaseViewModel
{
    protected readonly IPlayerService PlayerService;
    public TrackBaseViewModel CurrentSong { get; set; }
    public CollectionBaseViewModel CurrentAlbum { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;

    public PlayerTrackableViewModel(IPlayerService playerService)
    {
        PlayerService = playerService;
        playerService.PauseChanged += (_, isPaused) => IsPaused = isPaused;
        playerService.StopChanged += (_, isStopped) => IsStopped = isStopped;
        playerService.TrackValueProvider.ValueChanged += (_, newSong) => CurrentSong = newSong;

        PlaySongCommand = new(PlaySongAction, _ => true);
        PlayPauseCommand = new(PlayPauseAction, _ => true);
        LikeCommand = new(LikeAction, _ => true);
    }

    public DelegateCommand PlaySongCommand { get; set; }
    public DelegateCommand PlayPauseCommand { get; set; }
    public DelegateCommand LikeCommand { get; set; }

    protected virtual void PlaySongAction(object? parameter)
    {
        if (parameter is TrackBaseViewModel song)
        {
            PlayerService.SetAndPlay(song);
        }
    }

    protected virtual void PlayPauseAction(object? parameter)
    {
        PlayerService.Pause();
    }

    protected virtual void LikeAction(object? parameter)
    {
        switch (parameter)
        {
            case TrackBaseViewModel song:
                song.IsFavorite = !song.IsFavorite;
                break;
            case CollectionBaseViewModel album:
                album.IsFavorite = !album.IsFavorite;
                break;
        }
    }

    protected void TrySetSong()
    {
        var song = PlayerService.TrackValueProvider.Get();
        if (song is not null)
        {
            CurrentSong = song;
        }
        SetIsPausedAndIsStopped();
    }

    protected void SetIsPausedAndIsStopped()
    {
        IsPaused = PlayerService.IsPaused;
        IsStopped = PlayerService.IsStopped;
    }
}