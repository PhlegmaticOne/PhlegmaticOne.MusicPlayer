using System.Collections.ObjectModel;
using PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Navigation;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.Contracts.Services.ViewModelGet;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;

namespace PhlegmaticOne.MusicPlayer.Contracts.ApplicationViewModels;

public class TracksViewModel : PlayerTrackableViewModel
{
    private readonly IViewModelGetService _viewModelGetService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IUIThreadInvokerService _uiThreadInvokerService;
    private bool _isFirst = true;

    public ObservableCollection<TrackBaseViewModel> Songs { get; set; }
    public TracksViewModel(IPlayerService playerService, MusicNavigation<CollectionLinkViewModel, AlbumViewModel> collectionNavigation,
        IViewModelGetService viewModelGetService, ApplicationDbContext dbContext,
        IUIThreadInvokerService uiThreadInvokerService) : base(playerService)
    {
        _viewModelGetService = viewModelGetService;
        _dbContext = dbContext;
        _uiThreadInvokerService = uiThreadInvokerService;
        CollectionNavigation = collectionNavigation;
        Songs = new();
        FromDatabase();
    }
    public MusicNavigation<CollectionLinkViewModel, AlbumViewModel> CollectionNavigation { get; }

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
            var guids = _dbContext.Set<CollectionBase>().Select(x => x.Id);
            foreach (var guid in guids)
            {
                var mapped = await _viewModelGetService.GetViewModelAsync<TracksFromCollectionViewModel>(guid);
                await _uiThreadInvokerService.InvokeAsync(() =>
                {
                    foreach (var trackBaseViewModel in mapped.Tracks)
                    {
                        Songs.Add(trackBaseViewModel);
                    }
                });
            }
        });
    }
}