using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.Actions;
using PhlegmaticOne.MusicPlayer.Contracts.Models;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Like;
using PhlegmaticOne.MusicPlayer.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.ViewModels.EntityContainingViewModels;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.PlayerService.Models;
using PhlegmaticOne.WPF.Core.Commands;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.ViewModels;

public class SongQueueViewModel : PlayerTrackableViewModel
{
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    private readonly IEntityActionsProvider<TrackBaseViewModel> _trackActionsProvider;
    public ObservableCollection<TrackBaseViewModel> Songs { get; }
    public SongQueueViewModel(IPlayerService<TrackBaseViewModel> playerService, 
        ILikeService likeService, 
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        IEntityActionsProvider<TrackBaseViewModel> trackActionsProvider) : base(playerService, likeService)
    {
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        _trackActionsProvider = trackActionsProvider;
        Songs = new();
        PlayerService.EntitiesChanged += SongsQueueOnQueueChanged;

        ActiveArtistNavigationCommand = RelayCommandFactory.CreateAsyncCommand<ArtistLinkViewModel>(NavigateToActiveArtist, _ => true);
        ActiveCollectionNavigationCommand = RelayCommandFactory.CreateAsyncCommand<TrackBaseViewModel>(NavigateToActiveCollection, _ => true);

        TrySetSong();

        AddSongs(PlayerService);
    }
    public IRelayCommand ActiveArtistNavigationCommand { get; }
    public IRelayCommand ActiveCollectionNavigationCommand { get; }
    private void SongsQueueOnQueueChanged(object? sender, PlayerQueueChangedEventArgs<TrackBaseViewModel> e)
    {
        switch (e.CollectionChangedType)
        {
            case PlayerQueueChangedType.Added:
            {
                AddSongs(e.Entities);
                break;
            }
            case PlayerQueueChangedType.Removed:
            {
                RemoveSongs(e.Entities);
                break;
            }
        }
    }

    private async Task NavigateToActiveArtist(ArtistLinkViewModel? parameter)
    {
        if (parameter is null) return;
        
        await _entityContainingViewModelsNavigationService
            .From<ArtistLinkViewModel, ActiveArtistViewModel>()
            .NavigateAsync<ArtistViewModel>(parameter);
    }

    private async Task NavigateToActiveCollection(TrackBaseViewModel? parameter)
    {
        if(parameter is null) return;
        
        await _entityContainingViewModelsNavigationService
            .From<TrackBaseViewModel, ActiveAlbumViewModel>()
            .NavigateAsync<AlbumViewModel>(parameter);
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
}