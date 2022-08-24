using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;

public abstract class PlayerTrackableViewModel : ApplicationBaseViewModel
{
    protected readonly IPlayerService PlayerService;
    public TrackBaseViewModel CurrentSong { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;

    protected PlayerTrackableViewModel(IPlayerService playerService)
    {
        PlayerService = playerService;
        playerService.PauseChanged += (_, isPaused) => IsPaused = isPaused;
        playerService.StopChanged += (_, isStopped) => IsStopped = isStopped;
        playerService.TrackValueProvider.ValueChanged += (_, newSong) => CurrentSong = newSong;

        PlaySongCommand =  DelegateCommandFactory.CreateCommand(PlaySongAction, _ => true);
        PlayPauseCommand = DelegateCommandFactory.CreateCommand(PlayPauseAction, _ => true);
        LikeCommand = DelegateCommandFactory.CreateCommand(LikeAction, _ => true);
    }

    public IDelegateCommand PlaySongCommand { get; set; }
    public IDelegateCommand PlayPauseCommand { get; set; }
    public IDelegateCommand LikeCommand { get; set; }

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