using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Extensions;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.Entities.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.MusicPlayer.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;

public abstract class PlayerTrackableViewModel : ApplicationBaseViewModel, IDisposable
{
    protected readonly IPlayerService PlayerService;
    protected readonly ILikeService LikeService;
    public TrackBaseViewModel CurrentSong { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;

    protected PlayerTrackableViewModel(IPlayerService playerService, ILikeService likeService)
    {
        PlayerService = playerService;
        LikeService = likeService;
        playerService.PauseChanged += OnPlayerServiceOnPauseChanged;
        playerService.StopChanged += OnPlayerServiceOnStopChanged;
        playerService.TrackValueProvider.ValueChanged += OnTrackValueProviderOnValueChanged;

        PlaySongCommand =  DelegateCommandFactory.CreateCommand(PlaySongAction, _ => true);
        PlayPauseCommand = DelegateCommandFactory.CreateCommand(PlayPauseAction, _ => true);
        LikeCommand = DelegateCommandFactory.CreateCommand(LikeAction, _ => true);
        AddToQueueCommand = DelegateCommandFactory.CreateCommand(AddToQueue, _ => true);
    }

    private void OnTrackValueProviderOnValueChanged(object _, TrackBaseViewModel newSong)
    {
        CurrentSong = newSong;
    }

    private void OnPlayerServiceOnStopChanged(object _, bool isStopped)
    {
        IsStopped = isStopped;
    }

    private void OnPlayerServiceOnPauseChanged(object _, bool isPaused)
    {
        IsPaused = isPaused;
    }

    public IDelegateCommand PlaySongCommand { get; set; }
    public IDelegateCommand PlayPauseCommand { get; set; }
    public IDelegateCommand LikeCommand { get; set; }
    public IDelegateCommand AddToQueueCommand { get; }

    protected void AddToQueue(object? parameter)
    {
        if (parameter is TrackBaseViewModel trackBaseViewModel)
        {
            PlayerService.Enqueue(trackBaseViewModel.ToOneItemEnumerable(), false);
        }
    }

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

    protected virtual async void LikeAction(object? parameter)
    {
        if (parameter is not IIsFavorite isFavorite) return;

        var newLikeValue = !isFavorite.IsFavorite;
        isFavorite.IsFavorite = newLikeValue;

        switch (isFavorite)
        {
            case TrackBaseViewModel trackBaseViewModel:
            {
                await LikeService.SetNewLike<Song, TrackBaseViewModel>(trackBaseViewModel, newLikeValue);
                break;
            }
            case CollectionBaseViewModel collectionBaseViewModel:
            {
                await LikeService.SetNewLike<CollectionBase, CollectionBaseViewModel>(collectionBaseViewModel,
                    newLikeValue);
                break;
            }
            case ArtistBaseViewModel artistBaseViewModel:
            {
                await LikeService.SetNewLike<Artist, ArtistBaseViewModel>(artistBaseViewModel, newLikeValue);
                break;
            }
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

    public void Dispose()
    {
        PlayerService.PauseChanged -= OnPlayerServiceOnPauseChanged;
        PlayerService.StopChanged -= OnPlayerServiceOnStopChanged;
        PlayerService.TrackValueProvider.ValueChanged -= OnTrackValueProviderOnValueChanged;
    }
}