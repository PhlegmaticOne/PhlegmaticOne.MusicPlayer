using System.Threading.Tasks;
using PhlegmaticOne.MusicPlayer.Contracts.ViewModels.Base;
using PhlegmaticOne.MusicPlayer.UI.WPF.Services;
using PhlegmaticOne.MusicPlayer.UI.WPF.ViewModels;

namespace PhlegmaticOne.MusicPlayer.UI.WPF.ViewModelsFactories.Application;

public class SongQueueViewModelFactory : IMusicViewModelsFactory<EntityBaseViewModel, SongQueueViewModel>
{
    private readonly IPlayerService _playerService;

    public SongQueueViewModelFactory(IPlayerService playerService)
    {
        _playerService = playerService;
    }
    public Task<SongQueueViewModel> CreateViewModelAsync(EntityBaseViewModel entity)
    {
        return Task.FromResult(new SongQueueViewModel(_playerService));
    }
}