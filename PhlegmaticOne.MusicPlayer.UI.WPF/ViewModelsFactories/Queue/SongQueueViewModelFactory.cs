using PhlegmaticOne.MusicPlayer.Contracts.ViewModels;
using PhlegmaticOne.MusicPlayer.Players.Player;
using PhlegmaticOne.MusicPlayer.UI.WPF.Infrastructure;
using PhlegmaticOne.MusicPlayer.UI.WPF.PlayerHelpers;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.WPF.Core;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Queue;

public class SongQueueViewModelFactory : ISongQueueViewModelFactory
{
    private readonly IPlayerService _playerService;

    public SongQueueViewModelFactory(IPlayerService playerService)
    {
        _playerService = playerService;
    }
    public BaseViewModel CreateQueueViewModel()
    {
        return new SongQueueViewModel(_playerService);
    }
}