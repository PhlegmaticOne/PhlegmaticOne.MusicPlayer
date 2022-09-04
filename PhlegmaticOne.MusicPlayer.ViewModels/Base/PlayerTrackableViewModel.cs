using PhlegmaticOne.MusicPlayer.Contracts.Abstractions;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Models.Base;
using PhlegmaticOne.Players.Models;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Core.ViewModels;

namespace PhlegmaticOne.MusicPlayer.ViewModels.Base;

public abstract class PlayerTrackableViewModel : ApplicationBaseViewModel, IDisposable
{
    protected readonly IPlayerService<TrackBaseViewModel> PlayerService;
    protected readonly ILikeService LikeService;
    public TrackBaseViewModel? CurrentSong { get; set; }
    public bool IsPaused { get; set; } = true;
    public bool IsStopped { get; set; } = true;

    protected PlayerTrackableViewModel(IPlayerService<TrackBaseViewModel> playerService, ILikeService likeService)
    {
        PlayerService = playerService;
        LikeService = likeService;

        playerService.CurrentEntityChanged += PlayerServiceOnCurrentEntityChanged;
        playerService.Player.PlayerStateChanged += PlayerOnPlayerStateChanged;

        PlaySongCommand =  RelayCommandFactory.CreateCommand<TrackBaseViewModel>(PlaySongAction, _ => true);
        PlayPauseCommand = RelayCommandFactory.CreateCommand<TrackBaseViewModel>(PlayPauseAction, _ => true);
        AddToQueueCommand = RelayCommandFactory.CreateCommand<TrackBaseViewModel>(AddToQueue, _ => true);
        LikeCommand = RelayCommandFactory.CreateCommand(LikeAction, _ => true);
    }

    public IRelayCommand PlaySongCommand { get; set; }
    public IRelayCommand PlayPauseCommand { get; set; }
    public IRelayCommand LikeCommand { get; set; }
    public IRelayCommand AddToQueueCommand { get; }

    public void Dispose()
    {
        PlayerService.CurrentEntityChanged -= PlayerServiceOnCurrentEntityChanged;
        PlayerService.Player.PlayerStateChanged -= PlayerOnPlayerStateChanged;
    }
    protected void AddToQueue(TrackBaseViewModel? parameter)
    {
        if (parameter is null) return;

        PlayerService.Add(parameter);
    }

    protected virtual void PlaySongAction(TrackBaseViewModel? parameter)
    {
        if (parameter is null) return;

        PlayerService.SetAndPlay(parameter);
    }

    protected virtual void PlayPauseAction(TrackBaseViewModel? parameter)
    {
        if(parameter is null) return;
        
        PlayerService.Player.PauseOrUnpause();
    }

    protected virtual async void LikeAction(object? parameter)
    {
        if (parameter is not IIsFavorite isFavorite) return;

        var newLikeValue = !isFavorite.IsFavorite;
        //isFavorite.IsFavorite = newLikeValue;

        switch (isFavorite)
        {
            //case TrackBaseViewModel trackBaseViewModel:
            //{
            //    await LikeService.SetNewLike<Song, TrackBaseViewModel>(trackBaseViewModel, newLikeValue);
            //    break;
            //}
            //case CollectionBaseViewModel collectionBaseViewModel:
            //{
            //    await LikeService.SetNewLike<CollectionBase, CollectionBaseViewModel>(collectionBaseViewModel,
            //        newLikeValue);
            //    break;
            //}
            //case ArtistBaseViewModel artistBaseViewModel:
            //{
            //    await LikeService.SetNewLike<Artist, ArtistBaseViewModel>(artistBaseViewModel, newLikeValue);
            //    break;
            //}
        }
    }

    protected void TrySetSong()
    {
        var song = PlayerService.CurrentEntityInPlayer;
        if (song is not null)
        {
            CurrentSong = song;
        }
        SetIsPausedAndIsStopped();
    }

    protected void SetIsPausedAndIsStopped()
    {
        IsPaused = PlayerService.Player.PlayerState == PlayerState.Paused;
        IsStopped = PlayerService.Player.PlayerState == PlayerState.Stopped;
    }

    
    private void PlayerOnPlayerStateChanged(object? sender, PlayerState e)
    {
        switch (e)
        {
            case PlayerState.Paused:
            {
                IsPaused = true;
                break;
            }
            case PlayerState.Stopped:
            {
                IsStopped = true;
                IsPaused = true;
                break;
            }
            case PlayerState.Playing:
            {
                IsPaused = false;
                IsStopped = false;
                break;
            }
        }
    }

    private void PlayerServiceOnCurrentEntityChanged(object? sender, TrackBaseViewModel e) =>
        CurrentSong = e;
}