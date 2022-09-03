using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.Models.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.PlayerService.Base;
using PhlegmaticOne.WPF.Core.Commands;

namespace PhlegmaticOne.MusicPlayer.Contracts.Actions;

public class TrackActionsProvider : IEntityActionsProvider<TrackBaseViewModel>
{
    private readonly ILocalizationService _localizationService;
    private readonly IPlayerService<TrackBaseViewModel> _playerService;
    private readonly IFileOperatingService<TrackBaseViewModel> _trackDownloadService;
    private readonly IUIThreadInvokerService _uiThreadInvokerService;
    private readonly IRelayCommand _addToQueueCommand;
    private readonly IRelayCommand _removeFromCollectionCommand;
    private readonly IRelayCommand _downloadTrackCommand;
    private readonly IRelayCommand _deleteTrackCommand;
    public TrackActionsProvider(ILocalizationService localizationService,
        IPlayerService<TrackBaseViewModel> playerService, 
        IFileOperatingService<TrackBaseViewModel> trackDownloadService,
        IUIThreadInvokerService uiThreadInvokerService)
    {
        _localizationService = localizationService;
        _playerService = playerService;
        _trackDownloadService = trackDownloadService;
        _uiThreadInvokerService = uiThreadInvokerService;

        _addToQueueCommand = RelayCommandFactory.CreateAsyncCommand<TrackBaseViewModel>(AddToQueue, _ => true);
        _removeFromCollectionCommand = RelayCommandFactory.CreateAsyncCommand<TrackBaseViewModel>(RemoveFromCollection, _ => true);
        _downloadTrackCommand = RelayCommandFactory.CreateAsyncCommand<TrackBaseViewModel>(DownloadTrack, _ => true);
        _deleteTrackCommand = RelayCommandFactory.CreateAsyncCommand<TrackBaseViewModel>(DeleteTrack, _ => true);
    }
    public IDictionary<string, ICommand> GetActions(TrackBaseViewModel entity)
    {
        var addToQueueText = "Add to queue";
        var removeFromQueueText = "Remove from queue";
        var downloadTrackText = "Download track";
        var deleteTrackText = "Delete track";
        var result = new Dictionary<string, ICommand>();

        if (_playerService.Contains(entity))
        {
            result.Add(removeFromQueueText, _removeFromCollectionCommand);
        }
        else
        {
            result.Add(addToQueueText, _addToQueueCommand);
        }

        if (entity.IsDownloaded == false)
        {
            result.Add(downloadTrackText, _downloadTrackCommand);
        }
        else
        {
            result.Add(deleteTrackText, _deleteTrackCommand);
        }

        return result;
    }

    private async Task AddToQueue(TrackBaseViewModel? trackBaseViewModel)
    {
        if (trackBaseViewModel is null) return;

        _playerService.Add(trackBaseViewModel);

        await _uiThreadInvokerService.InvokeAsync(() =>
        {
            UpdateTrackActions(trackBaseViewModel);
        });
    }

    private async Task RemoveFromCollection(TrackBaseViewModel? trackBaseViewModel)
    {
        if (trackBaseViewModel is null) return;

        if (_playerService.Remove(trackBaseViewModel))
        {
            await _uiThreadInvokerService.InvokeAsync(() =>
            {
                UpdateTrackActions(trackBaseViewModel);
            });
        }
    }

    private async Task DeleteTrack(TrackBaseViewModel? trackBaseViewModel)
    {
        if (trackBaseViewModel is null) return;

        await _trackDownloadService.Delete(trackBaseViewModel);

        await _uiThreadInvokerService.InvokeAsync(() =>
        {
            trackBaseViewModel.IsDownloaded = false;
            UpdateTrackActions(trackBaseViewModel);
        });
    }

    private async Task DownloadTrack(TrackBaseViewModel? trackBaseViewModel)
    {
        if (trackBaseViewModel is null) return;

        await _uiThreadInvokerService.InvokeAsync(() =>
        {
            trackBaseViewModel.IsDownloading = true;
        });

        await _trackDownloadService.Download(trackBaseViewModel);

        await _uiThreadInvokerService.InvokeAsync(() =>
        {
            trackBaseViewModel.IsDownloading = false;
            trackBaseViewModel.IsDownloaded = true;
            UpdateTrackActions(trackBaseViewModel);
        });
    }

    
    private void UpdateTrackActions(TrackBaseViewModel trackBaseViewModel)
    {
        trackBaseViewModel.Actions = GetActions(trackBaseViewModel);
    }
}