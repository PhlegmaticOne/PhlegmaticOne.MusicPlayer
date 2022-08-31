using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.Actions;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationQueue;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class SongQueueViewModel : PlayerTrackableViewModel
{
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    private readonly IEntityActionsProvider<TrackBaseViewModel> _trackActionsProvider;
    public ObservableCollection<TrackBaseViewModel> Songs { get; }
    public SongQueueViewModel(IPlayerService playerService, ILikeService likeService, 
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider) : base(playerService, likeService)
    {
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        _trackActionsProvider = trackActionsProvider;
        Songs = new();
        PlayerService.QueueChanged += SongsQueueOnQueueChanged;
        PlayerService.RaiseEvents();
        ActiveArtistNavigationCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveArtist, _ => true);
        ActiveCollectionNavigationCommand = DelegateCommandFactory.CreateCommand(NavigateToActiveCollection, _ => true);
        TrySetSong();
    }
    public IDelegateCommand ActiveArtistNavigationCommand { get; }
    public IDelegateCommand ActiveCollectionNavigationCommand { get; }
    private void SongsQueueOnQueueChanged(object? sender, CollectionChangedEventArgs<TrackBaseViewModel> e)
    {
        switch (e.CollectionChangedType)
        {
            case CollectionChangedType.Added:
            {
                AddSongs(e.Entities);
                break;
            }
            case CollectionChangedType.Removed:
            {
                RemoveSongs(e.Entities);
                break;
            }
        }
    }

    private void AddSongs(IEnumerable<TrackBaseViewModel> songs)
    {
        foreach (var entity in songs)
        {
            entity.Actions = _trackActionsProvider.GetActions(entity);
            Songs.Add(entity);
        }
        TrySetSong();
    }

    private void RemoveSongs(IEnumerable<TrackBaseViewModel> songs)
    {
        foreach (var entity in songs)
        {
            entity.Actions = _trackActionsProvider.GetActions(entity);
            Songs.Remove(entity);
        }
    }



    private async void NavigateToActiveArtist(object? parameter)
    {
        if (parameter is ArtistLinkViewModel artistLinkViewModel)
        {
            await _entityContainingViewModelsNavigationService
                .From<ArtistLinkViewModel, ActiveArtistViewModel>()
                .NavigateAsync<ArtistViewModel>(artistLinkViewModel);
        }
    }

    private async void NavigateToActiveCollection(object? parameter)
    {
        if (parameter is TrackBaseViewModel trackBaseViewModel)
        {
            await _entityContainingViewModelsNavigationService
                .From<TrackBaseViewModel, ActiveAlbumViewModel>()
                .NavigateAsync<AlbumViewModel>(trackBaseViewModel);
        }
    }
}