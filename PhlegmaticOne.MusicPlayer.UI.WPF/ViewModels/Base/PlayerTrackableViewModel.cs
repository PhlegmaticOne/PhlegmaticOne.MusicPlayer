using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

public class PlayerTrackableViewModel : BaseViewModel
{
    protected readonly IPlayerService PlayerService;
    public SongEntityViewModel CurrentSong { get; set; }
    public CollectionBaseViewModel CurrentAlbum { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;

    public PlayerTrackableViewModel(IPlayerService playerService)
    {
        PlayerService = playerService;
        playerService.PauseChanged += (_, isPaused) => IsPaused = isPaused;
        playerService.StopChanged += (_, isStopped) => IsStopped = isStopped;
        playerService.ValueProvider<SongEntityViewModel>()!.ValueChanged += (_, newSong) => CurrentSong = newSong;

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
        var song = PlayerService.ValueProvider<SongEntityViewModel>()!.Get();
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
        IsPaused = PlayerService.IsPaused;
        IsStopped = PlayerService.IsStopped;
    }
}