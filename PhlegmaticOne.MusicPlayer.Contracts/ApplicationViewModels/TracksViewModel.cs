using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.EntityContainingViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Collections;
using PhlegmaticOne.MusicPlayer.WPF.Core;
using PhlegmaticOne.WPF.Navigation;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class TracksViewModel : PlayerTrackableViewModel
{
    private readonly IEntityContainingViewModelsNavigationService _entityContainingViewModelsNavigationService;
    private readonly IEntityCollectionGetService _viewModelGetService;
    private readonly IUIThreadInvokerService _uiThreadInvokerService;
    private bool _isFirst = true;

    public ObservableCollection<TrackBaseViewModel> Songs { get; set; }
    public TracksViewModel(IPlayerService playerService, 
        IEntityContainingViewModelsNavigationService entityContainingViewModelsNavigationService,
        IEntityCollectionGetService viewModelGetService,
        IUIThreadInvokerService uiThreadInvokerService) : base(playerService)
    {
        _entityContainingViewModelsNavigationService = entityContainingViewModelsNavigationService;
        _viewModelGetService = viewModelGetService;
        _uiThreadInvokerService = uiThreadInvokerService;
        Songs = new();
        ActiveArtistNavigationCommand = new(NavigateToActiveArtist, _ => true);
        ActiveCollectionNavigationCommand = new(NavigateToActiveCollection, _ => true);
        FromDatabase();
    }
    public DelegateCommand ActiveArtistNavigationCommand { get; }
    public DelegateCommand ActiveCollectionNavigationCommand { get; }
    protected override void PlaySongAction(object? parameter)
    {
        if (_isFirst)
        {
            PlayerService.Enqueue(Songs, true);
            _isFirst = false;
        }
        base.PlaySongAction(parameter);
    }

    private async void FromDatabase()
    {
        await Task.Run(async () =>
        {
            var mapped = await _viewModelGetService.GetEntityCollectionAsync<AllTracksViewModel>();
            await _uiThreadInvokerService.InvokeAsync(() =>
            {
                foreach (var trackBaseViewModel in mapped.Tracks)
                {
                    Songs.Add(trackBaseViewModel);
                }
            });
        });
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