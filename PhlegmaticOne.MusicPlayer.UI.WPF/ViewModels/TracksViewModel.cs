﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PhlegmaticOne.MusicPlayer.Contracts.Services;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.Data.Context;
using PhlegmaticOne.MusicPlayer.Entities;
using PhlegmaticOne.MusicPlayer.UI.WPF.Navigation;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

public class TracksViewModel : PlayerTrackableViewModel
{
    private readonly IViewModelGetService _viewModelGetService;
    private readonly ApplicationDbContext _dbContext;
    private bool _isFirst = true;

    public ObservableCollection<TrackBaseViewModel> Songs { get; set; }
    public TracksViewModel(IPlayerService playerService, MusicNavigation<CollectionLinkViewModel, AlbumViewModel> collectionNavigation,
        IViewModelGetService viewModelGetService, ApplicationDbContext dbContext) : base(playerService)
    {
        _viewModelGetService = viewModelGetService;
        _dbContext = dbContext;
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
                await UIThreadInvoker.InvokeAsync(() =>
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