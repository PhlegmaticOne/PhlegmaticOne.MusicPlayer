using System.Collections.ObjectModel;
using System.Windows.Input;
using PhlegmaticOne.MusicPlayer.Contracts.EntityViewModels.Base;
using PhlegmaticOne.MusicPlayer.Contracts.Extensions;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Download;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Localization;
using PhlegmaticOne.MusicPlayer.Contracts.Services.Player;
using PhlegmaticOne.MusicPlayer.Contracts.Services.UI;
using PhlegmaticOne.MusicPlayer.WPF.Core.Commands;

namespace PhlegmaticOne.MusicPlayer.Contracts.Actions;

public class TrackActionsProvider : IEntityActionsProvider<TrackBaseViewModel>
{
    private readonly ILocalizationService _localizationService;
    private readonly IPlayerService _playerService;
    private readonly IFileOperatingService<TrackBaseViewModel> _trackDownloadService;
    private readonly IUIThreadInvokerService _uiThreadInvokerService;
    private readonly IDelegateCommand _addToQueueCommand;
    private readonly IDelegateCommand _removeFromCollectionCommand;
    private readonly IDelegateCommand _downloadTrackCommand;
    private readonly IDelegateCommand _deleteTrackCommand;
    public TrackActionsProvider(ILocalizationService localizationService,
        IPlayerService playerService, 
        IFileOperatingService<TrackBaseViewModel> trackDownloadService,
        IUIThreadInvokerService uiThreadInvokerService)
    {
        _localizationService = localizationService;
        _playerService = playerService;
        _trackDownloadService = trackDownloadService;
        _uiThreadInvokerService = uiThreadInvokerService;

        _addToQueueCommand = DelegateCommandFactory.CreateCommand(AddToQueue, _ => true);
        _removeFromCollectionCommand = DelegateCommandFactory.CreateCommand(RemoveFromCollection, _ => true);
        _downloadTrackCommand = DelegateCommandFactory.CreateCommand(DownloadTrack, _ => true);
        _deleteTrackCommand = DelegateCommandFactory.CreateCommand(DeleteTrack, _ => true);
    }
    public IDictionary<string, ICommand> GetActions(TrackBaseViewModel entity)
    {
        var addToQueueText = "Add to queue";
        var removeFromQueueText = "Remove from queue";
        var downloadTrackText = "Download track";
        var deleteTrackText = "Delete track";
        var result = new Dictionary<string, ICommand>();

        if (_playerService.TracksQueue.Contains(entity))
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

    private void AddToQueue(object? parameter)
    {
        if (parameter is not TrackBaseViewModel trackBaseViewModel) return;

        _playerService.Enqueue(trackBaseViewModel.ToOneItemEnumerable(), false);
        UpdateTrackActions(trackBaseViewModel);
    }

    private void RemoveFromCollection(object? parameter)
    {
        if (parameter is not TrackBaseViewModel trackBaseViewModel) return;

        _playerService.TracksQueue.Remove(trackBaseViewModel);
        UpdateTrackActions(trackBaseViewModel);
    }

    private async void DownloadTrack(object? parameter)
    {
        if (parameter is not TrackBaseViewModel trackBaseViewModel) return;

        await _uiThreadInvokerService.InvokeAsync(async () =>
        {
            trackBaseViewModel.IsDownloading = true;
            await _trackDownloadService.Download(trackBaseViewModel);
            trackBaseViewModel.IsDownloading = false;
            trackBaseViewModel.IsDownloaded = true;
            UpdateTrackActions(trackBaseViewModel);
        });
    }

    private async void DeleteTrack(object? parameter)
    {
        if (parameter is not TrackBaseViewModel trackBaseViewModel) return;

        await _uiThreadInvokerService.InvokeAsync(() =>
        {
            _trackDownloadService.Delete(trackBaseViewModel);
            trackBaseViewModel.IsDownloaded = false;
            UpdateTrackActions(trackBaseViewModel);
        });
    }

    private void UpdateTrackActions(TrackBaseViewModel trackBaseViewModel)
    {
        trackBaseViewModel.Actions = GetActions(trackBaseViewModel);
    }
}