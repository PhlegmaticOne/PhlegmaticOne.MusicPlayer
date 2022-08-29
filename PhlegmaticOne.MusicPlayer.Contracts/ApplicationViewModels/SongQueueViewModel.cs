using System.Collections.ObjectModel;
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

public class SongQueueViewModel : PlayerTrackableViewModel, IDisposable
{
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    public ObservableCollection<TrackBaseViewModel> Songs { get; }
    public SongQueueViewModel(IPlayerService playerService, ILikeService likeService, 
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService) : base(playerService, likeService)
    {
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
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
            Songs.Add(entity);
        }
    }

    private void RemoveSongs(IEnumerable<TrackBaseViewModel> songs)
    {
        foreach (var entity in songs)
        {
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

    public new void Dispose()
    {
        base.Dispose();
        PlayerService.QueueChanged -= SongsQueueOnQueueChanged;
    }
}